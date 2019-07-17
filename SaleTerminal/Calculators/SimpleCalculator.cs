using System;
using System.Collections.Generic;
using System.Linq;
using TProduct = System.String;


namespace SaleTerminal
{
	public class SimpleCalculator : IPriceCalculator
	{
		public CalculateState TakeSuitableProducts(
			IEnumerable<Price> pricing, 
			CalculateState calculateState)
		{
			var leftProducts = new LinkedList<TProduct>();
			decimal totalPrice = calculateState.LeftProducts.Sum(product => {
				var simplePrice = pricing.FirstOrDefault(price => 
					(price is SimplePrice)
					&& price.Product.Equals(product)) as SimplePrice;

				if (simplePrice == null) {
					leftProducts.AddLast(product);
					return 0m;
				}
				return simplePrice.Price;
			});

			return new CalculateState(calculateState.Money + totalPrice, leftProducts);
		}
	}
}
