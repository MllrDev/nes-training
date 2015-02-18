using System;

namespace SimpleEventStore.Domain.Events
{
	public class AnagraficaArticoloCensita
	{
        public string Id { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public string Uom { get; private set; }
        public decimal SafetyStockLevel { get; private set; }

        public AnagraficaArticoloCensita(string id, string code, string description, string uom, decimal safetyStockLevel)
		{
			Id = id;
			Code = code;
			Description = description;
			Uom = uom;
            SafetyStockLevel = safetyStockLevel;
		}
	}
}