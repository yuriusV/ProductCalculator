using System;
using System.Collections.Generic;
using System.Linq;
using TProduct = System.String;


namespace SaleTerminal
{
	public class SimpleCalculator : IPriceCalculator
	{
		public CalculateState TakeAndCalculate(
			IEnumerable<Price> pricing, 
			CalculateState calculateState)
		{
			double totalPrice = calculateState.LeftProducts.Sum(product => {
				var simplePrice = pricing.FirstOrDefault(price => 
					(price is SimplePrice) 
					&& price.Product.Equals(product)) as SimplePrice;

				return simplePrice.Price;
			});

			return new CalculateState(calculateState.Money + totalPrice, Enumerable.Empty<TProduct>());
		}
	}
}
