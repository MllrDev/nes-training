using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using SimpleEventStore.Domain;
using SimpleEventStore.Domain.Events;
using SimpleEventStore.Eventstore;
using SharpTestsEx;

namespace SimpleEventStore.Tests
{
    [TestFixture]
    public class AnagraficaArticoloTests
    {
        [Test]
        public void censisci_AnagraficaArticolo()
        {
            var item = new AnagraficaArticolo();
            item.Censisci(TestConfig.Id, "001", "SSD Crucial M4 256GB", "NR", 50);

            Assert.AreEqual(1, item.Events.Count);
        }

        [Test]
        public void Scarica_diminuisce_la_GiacenzaAttuale_se_disponibile()
        {
            var stato = new AnagraficaArticolo.StatoAnagraficaArticolo()
            {
                Disabilitato = false,
                GiacenzaAttuale = 20,
                ScortaMinima = 20
            };
            var sut = new AnagraficaArticolo(stato);
            sut.Scarica(11);
            Assert.AreEqual(9, sut.stato.GiacenzaAttuale);
        }

        [Test]
        public void Scarica_genera_eccezione_se_la_quantità_richiesta_non_è_in_giacenza()
        {
            var stato = new AnagraficaArticolo.StatoAnagraficaArticolo()
            {
                Disabilitato = false,
                GiacenzaAttuale = 10,
                ScortaMinima = 20
            };
            var sut = new AnagraficaArticolo(stato);
            Executing.This(() => sut.Scarica(11))
                .Should()
                .Throw<ArgumentException>()
                .And
                .ValueOf
                .ParamName
                .Should()
                .Be
                .EqualTo("quantitàDaScaricare");
        }

        [Test]
        public void save_item_to_eventStream()
        {
            var item = new AnagraficaArticolo();
            item.Censisci(TestConfig.Id, "001", "SSD Crucial M4 256GB", "NR", 100);
            var stream = new EventStream();
            item.Save(stream);

            Assert.AreEqual(1, item.Version);
            Assert.AreEqual(1, stream.Events.Count);
        }

        [Test]
        public void load_item_from_eventStream()
        {
            var stream = new EventStream
                             {
                                 Events =
                                     new List<object>(new[]
						                 {
						                     new AnagraficaArticoloCensita(TestConfig.Id, "001", "SSD Crucial M4 256GB", "NR", 100)
						                 }),
                                 Version = 1
                             };

            var item = AggregateBase.Load<AnagraficaArticolo>(stream);

            Assert.AreEqual(1, item.Version);
            Assert.AreEqual(TestConfig.Id, item.Id);
        }

        [Test]
        public void save_to_repository()
        {
            // Arrange
            object dispatchedEvent = null;
            var dispatcher = new Action<object>((evt) =>
                {
                    dispatchedEvent = evt;
                });

            var repository = new Repository(eventsDispatcher: dispatcher);

            string fname = repository.MakeAggregateStreamFileName(TestConfig.Id);
            if (File.Exists(fname))
                File.Delete(fname);

            var item = new AnagraficaArticolo();
            item.Censisci(TestConfig.Id, "001", "SSD Crucial M4 256GB", "NR", 100);
            
            // Act
            repository.Save(item);

            // Assert
            Assert.IsTrue(File.Exists(fname));
            Assert.IsNotNull(dispatchedEvent);
            Assert.IsTrue(dispatchedEvent is AnagraficaArticoloCensita);
        }

        //[Test]
        //public void load_from_repository()
        //{
        //    var repository = new Repository(TestConfig.PreloadedStore);
        //    var item = repository.GetById<AnagraficaArticolo>(TestConfig.Id);

        //    Assert.IsNotNull(item);
        //    Assert.AreEqual(TestConfig.Id, item.Id);

        //    Assert.AreEqual(0, item.InStock);
        //    Assert.IsTrue(item.Disabled);
        //}

        //[Test]
        //public void create_a_stream_with_few_events()
        //{
        //    var item = new AnagraficaArticolo(); // AnagraficaArticolo(TestConfig.Id, "SN0001", "Snacks", "NR", 100);

        //    item.Disable();
        //    Assert.IsTrue(item.Disabled);

        //    var repository = new Repository(TestConfig.TestStore);
        //    repository.Save(item);

        //    // check json file
        //}
    }
}
