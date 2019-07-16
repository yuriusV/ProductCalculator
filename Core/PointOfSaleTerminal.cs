using System.Collections.Generic;
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

		public double CalculateTotal()
		{
			var moneyResult = _simpleCalculator.TakeSuitableProducts(_pricing,
				_packsCalculator.TakeSuitableProducts(_pricing,
					new CalculateState(0, _products))
			).Money;

			ResetInitialState();

			return moneyResult;
		}

		private void ResetInitialState() {
			_products = new LinkedList<TProduct>();
		}
	}
}
