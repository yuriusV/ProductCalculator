namespace WebApi.Services {
	using SaleTerminal;
	using WebApi.Interfaces;
	using System.Collections.Generic;

	public class InMemoryPricesRepository: IPricesRepository
	{
		public IEnumerable<Price> GetPrices() {
			return new List<Price> {
				new SimplePrice("A", 1.0m),
				new SimplePrice("B", 2.0m),
				new ProductPackPrice("A", 2, 1.5m)
			};
		}
	}
}