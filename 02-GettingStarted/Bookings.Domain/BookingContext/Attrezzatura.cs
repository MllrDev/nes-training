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
        public string Codice { get; private set; }
        public string Descrizione { get; private set; }

        public Attrezzatura()
        {

        }

        public void Censisci(Guid id, string codice, string descrizione)
        {
            if(this.Id==Guid.Empty)
            {
                this.RaiseEvent(new AttrezzaturaCensita(id, codice, descrizione));
            }
            else
            {
                throw new ArgumentException("", "id");
            }
        }

        public void Apply(AttrezzaturaCensita @event)
        {
            this.Id = @event.AttrezzaturaId;
            this.Codice = @event.Codice;
            this.Descrizione = @event.Descrizione;
        }
    }
}
