using Bookings.Domain.BookingContext;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookings.Tests.DomainTests
{
    [Subject(typeof(Attrezzatura))]
    public class se_provo_a_censire_una_attrezzatura_già_censita
    {
        private static Attrezzatura attrezzatura;
        private static Guid id = Guid.NewGuid();
        private static string codice = "001";
        private static string descrizione = "Sala riunioni";
        private static Exception errore;

        Establish context = () =>
        {
            attrezzatura = new Attrezzatura();
            attrezzatura.Censisci(id, codice, descrizione);
        };

        Because of = () =>
            {
                errore = Catch.Exception(() => attrezzatura.Censisci(id, codice, descrizione));
            };

        It genera_un_errore = () =>
            {
                errore.ShouldBeOfExactType<ArgumentException>();   
            };
    }
}
