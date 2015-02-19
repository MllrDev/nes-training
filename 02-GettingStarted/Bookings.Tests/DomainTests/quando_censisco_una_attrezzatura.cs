using Bookings.Domain.BookingContext;
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
        private static  Attrezzatura attrezzatura;

        Establish context = () =>
        {
            attrezzatura = new Attrezzatura();
        };

        Because of = () =>
        {
            attrezzatura.Censisci("001", "Sala riunioni");
        };
    }
}
