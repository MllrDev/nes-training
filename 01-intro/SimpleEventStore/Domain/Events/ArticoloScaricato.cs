using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventStore.Domain.Events
{
    public class ArticoloScaricato
    {
        public string Id { get; private set; }
        public int QuantitàScaricata { get; private set; }

        public ArticoloScaricato(string id, int quantitàScaricata)
        {
            this.Id = id;
            this.QuantitàScaricata = quantitàScaricata;
        }
    }
}
