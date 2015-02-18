using System;
using SimpleEventStore.Domain.Events;
using SimpleEventStore.Eventstore;

namespace SimpleEventStore.Domain
{
    public class AnagraficaArticolo : AggregateBase
    {
        private decimal SafetyStockLevel { get; set; }
        internal decimal InStock { get; private set; }
        internal bool Disabled { get; private set; }

        public AnagraficaArticolo()
        {
        }

        public void Censisci(string id, string code, string description, string uom, decimal safetyStockLevel)
        {
            RaiseEvent(new AnagraficaArticoloCensita(id, code, description, uom, safetyStockLevel));
        }

        public void Disable()
        {
            if (Disabled)
                return;
            
            RaiseEvent(new AnagraficaArticoloDisattivata(Id));
        }

        public void Apply(AnagraficaArticoloCensita evt)
        {
            Id = evt.Id;
            SafetyStockLevel = evt.SafetyStockLevel;
        }

        public void Apply(AnagraficaArticoloDisattivata evt)
        {
            Disabled = true;
        }
    }
}