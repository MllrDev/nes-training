using Bookings.Domain.BookingContext.Events;
using CommonDomain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookings.Domain.BookingContext
{
    public class Attrezzatura : AggregateBase
    {
        public Attrezzatura()
        {

        }

        public void Censisci(string codice, string descrizione)
        {
            this.RaiseEvent(new AttrezzaturaCensita(codice, descrizione));
        }

        public void Apply(AttrezzaturaCensita @event)
        {

        }
    }
}
