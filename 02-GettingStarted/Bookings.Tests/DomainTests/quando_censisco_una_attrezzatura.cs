using Bookings.Domain.BookingContext;
using Bookings.Domain.BookingContext.Events;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookings.Tests.DomainTests
{
    [Subject(typeof(Attrezzatura))]
    public class quando_censisco_una_attrezzatura
    {
        private static Attrezzatura attrezzatura;
        private static Guid id = Guid.NewGuid();
        private static string codice = "001";
        private static string descrizione = "Sala riunioni";


        Establish context = () =>
        {
            attrezzatura = new Attrezzatura();
        };

        Because of = () =>
        {
            attrezzatura.Censisci(id, codice, descrizione);
        };

        It non_deve_essere_già_censita = () =>
            {
                attrezzatura.ShouldHadRaised<AttrezzaturaCensita>();
                attrezzatura.LastEventOfType<AttrezzaturaCensita>().Codice.ShouldBeEqualIgnoringCase("001");
                attrezzatura.LastEventOfType<AttrezzaturaCensita>().Descrizione.ShouldBeLike("Sala riunioni");
            };
    }
}
