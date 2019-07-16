using System.Collections.Generic;
using TProduct = System.String;


namespace SaleTerminal
{
	public class PointOfSaleTerminal {
		private LinkedList<TProduct> _products;
		private IPriceCalculator _firstCalculator;
		private IPriceCalculator _secondCalculator;
		private IEnumerable<Price> _pricing;

		public PointOfSaleTerminal() {
			_firstCalculator = new ProductPackCalculator();
			_secondCalculator = new SimpleCalculator();
			_products = new LinkedList<TProduct>();
		}

		public void SetPricing(IEnumerable<Price> pricing) {
			_pricing = pricing;
		}

		public void Scan(string productName) {
			_products.AddFirst(productName);
		}

		public double CalculateTotal() {
			return _secondCalculator.TakeAndCalculate(_pricing, 
				_firstCalculator.TakeAndCalculate(_pricing, 
					new CalculateState(0, _products))
			).Money;
		}
	}
}
