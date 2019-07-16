using System.Collections.Generic;
using System.Linq;
using TProduct = System.String;


namespace SaleTerminal
{
	public class ProductPackCalculator : IPriceCalculator
	{
		public CalculateState TakeAndCalculate(
			IEnumerable<Price> pricing, 
			CalculateState calculateState)
		{
			var productGroups = calculateState.LeftProducts.GroupBy(x => x);
			
			var pricingGroupedAroundProduct = pricing.GroupBy(x => x.Product);
			foreach(var productPricing in pricingGroupedAroundProduct) {
				foreach(var priceVariant in GetSortedByProfitPackPrice(productPricing)) {
					var productGroupsForVariant = productGroups.Where(x => x.Key == priceVariant.Product 
						&& x.Count() >= priceVariant.CountProducts);
					

				}
			}


			return new CalculateState(calculateState.Money + 0, Enumerable.Empty<TProduct>());
		}

		private IEnumerable<ProductPackPrice> GetSortedByProfitPackPrice(IEnumerable<Price> prices) {
			return prices
					.Where(x => x is ProductPackPrice)
					.Select(x => x as ProductPackPrice)
					.OrderBy(x => {
						return x.PriceForPack / x.CountProducts;
					});
		}
	}
}
