using System;
using SimpleEventStore.Domain.Events;
using SimpleEventStore.Eventstore;

namespace SimpleEventStore.Domain
{
    public class AnagraficaArticolo : AggregateBase
    {
        internal StatoAnagraficaArticolo stato;

        public AnagraficaArticolo()
        {
            stato = new StatoAnagraficaArticolo();
        }

        public AnagraficaArticolo(StatoAnagraficaArticolo stato)
        {
            this.stato = stato;
        }

        public void Censisci(string id, string code, string description, string uom, decimal safetyStockLevel)
        {
            RaiseEvent(new AnagraficaArticoloCensita(id, code, description, uom, safetyStockLevel));
        }

        public void Scarica(int quantitàDaScaricare)
        { 
            if(quantitàDaScaricare <= this.stato.GiacenzaAttuale)
            {
                var e = new ArticoloScaricato(this.Id, quantitàDaScaricare);
                RaiseEvent(e);
            }
            else
            {
                throw new ArgumentException("", "quantitàDaScaricare");
            }
        }

        public void Disable()
        {
            if (stato.Disabilitato)
                return;
            
            RaiseEvent(new AnagraficaArticoloDisattivata(Id));
        }

        public void Apply(AnagraficaArticoloCensita evt)
        {
            Id = evt.Id;
            stato.ScortaMinima = evt.SafetyStockLevel;
        }

        public void Apply(ArticoloScaricato evt)
        {
            this.stato.GiacenzaAttuale -= evt.QuantitàScaricata;
        }

        public void Apply(AnagraficaArticoloDisattivata evt)
        {
            stato.Disabilitato = true;
        }

        public class StatoAnagraficaArticolo
        {
            public decimal ScortaMinima { get; set; }
            public bool Disabilitato { get; set; }
            public int GiacenzaAttuale { get; set; }
        }
    }
}