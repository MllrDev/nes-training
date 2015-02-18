using System;

namespace SimpleEventStore.Domain.Events
{
    public class AnagraficaArticoloDisattivata
    {
        public string Id { get; private set; }

        public AnagraficaArticoloDisattivata(string id)
        {
            Id = id;
        }
    }
}