using Bookings.Domain.BookingContext.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookings.Domain.BookingContext.Events
{
    public class AttrezzaturaCensita : Event
    {
        public Guid AttrezzaturaId { get; private set; }
        public string Codice { get; private set; }
        public string Descrizione { get; private set; }

        public AttrezzaturaCensita(Guid id, string codice, string descrizione)
        {
            this.AttrezzaturaId = id;
            this.Codice = codice;
            this.Descrizione = descrizione;
        }
    }
}
