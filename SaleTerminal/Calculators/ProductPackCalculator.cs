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
			var productGroups = calculateState.LeftProducts.GroupBy(x => x)
				.ToDictionary(x => x.Key, x => x.Count());
			var pricingGroupedAroundProduct = pricing.GroupBy(x => x.Product);
			decimal moneyTaken = 0m;

			foreach (var productPricing in pricingGroupedAroundProduct)
			{
				foreach (var priceVariant in GetSortedByProfitPackPrices(productPricing))
				{
					var productGroupsForVariant = productGroups.Keys.Where(productName => productName == priceVariant.Product
						&& productGroups[productName] >= priceVariant.CountProducts).ToList();

					foreach (var product in productGroupsForVariant)
					{
						while (productGroups[product] >= priceVariant.CountProducts)
						{
							productGroups[product] -= priceVariant.CountProducts;
							moneyTaken += priceVariant.PriceForPack;
						}
					}
				}
			}

			var leftProducts = new List<TProduct>();
			foreach (var key in productGroups.Keys)
			{
				leftProducts.AddRange(Enumerable.Repeat(key, productGroups[key]));
			}
			return new CalculateState(calculateState.Money + moneyTaken, leftProducts);
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
