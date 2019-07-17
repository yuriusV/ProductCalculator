namespace WebApi.Services 
{
	using WebApi.Interfaces;
	using SaleTerminal;
	using System.Collections.Generic;
	public class PointOfSaleTerminalService: IPointOfSaleTerminalService {

		private readonly PointOfSaleTerminal _terminal;
		private readonly IPricesRepository _pricesRepository;
		public PointOfSaleTerminalService(IPricesRepository pricesRepository) {
			_terminal = new PointOfSaleTerminal();
			_pricesRepository = pricesRepository;
		}
		public decimal CalculatePrice(IEnumerable<string> inputs) {
			_terminal.SetPricing(_pricesRepository.GetPrices());
			foreach(var productName in inputs) {
				_terminal.Scan(productName);
			}

			return _terminal.CalculateTotal();
		}
	}
}