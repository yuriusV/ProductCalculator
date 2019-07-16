using System.Collections.Generic;
using System.Linq;
using TProduct = System.String;


namespace SaleTerminal
{
	public class ProductPackCalculator : IPriceCalculator
	{
		public CalculateState TakeSuitableProducts(
			IEnumerable<Price> pricing,
			CalculateState calculateState)
		{
			var productGroups = calculateState.LeftProducts.GroupBy(x => x);

			var pricingGroupedAroundProduct = pricing.GroupBy(x => x.Product);
			foreach (var productPricing in pricingGroupedAroundProduct)
			{
				foreach (var priceVariant in GetSortedByProfitPackPrices(productPricing))
				{
					var productGroupsForVariant = productGroups.Where(group => group.Key == priceVariant.Product
						&& group.Count() >= priceVariant.CountProducts);


				}
			}

			return new CalculateState(calculateState.Money + 0, Enumerable.Empty<TProduct>());
		}

		private IEnumerable<ProductPackPrice> GetSortedByProfitPackPrices(IEnumerable<Price> prices)
		{
			return prices
				.Where(p => p is ProductPackPrice)
				.Select(p => p as ProductPackPrice)
				.OrderBy(p =>
				{
					return p.PriceForPack / p.CountProducts;
				});
		}
	}
}
