using System.Collections.Generic;
using System.Linq;
using TProduct = System.String;


namespace SaleTerminal
{
	public class PointOfSaleTerminal
	{
		private readonly IPriceCalculator _packsCalculator;
		private readonly IPriceCalculator _simpleCalculator;
		private LinkedList<TProduct> _products;
		private IEnumerable<Price> _pricing;

		public PointOfSaleTerminal()
		{
			_packsCalculator = new ProductPackCalculator();
			_simpleCalculator = new SimpleCalculator();

			ResetInitialState();
		}

		public void SetPricing(IEnumerable<Price> pricing)
		{
			_pricing = pricing;
		}

		public void Scan(string productName)
		{
			_products.AddLast(productName);
		}

		public decimal? CalculateTotal()
		{
			var calculateResult = _simpleCalculator.TakeSuitableProducts(_pricing,
				_packsCalculator.TakeSuitableProducts(_pricing,
					new CalculateState(0m, _products))
			);

			ResetInitialState();

			// Calculators cannot check all products using current price list
			if (calculateResult.LeftProducts.Any()) {
				return null;
			}

			
			return calculateResult.Money;
		}

		private void ResetInitialState() {
			_products = new LinkedList<TProduct>();
		}
	}
}
