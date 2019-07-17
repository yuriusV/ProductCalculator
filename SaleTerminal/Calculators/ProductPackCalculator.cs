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


			var calculatingResult = productGroups.AsParallel().Select(product => {
				var productPricings = GetPricesToCheckForProduct(product.Key, pricingGroupedAroundProduct);
				return TakeProduct(product.Key, product.Value, productPricings);
			}).Aggregate(
				(leftProducts: Enumerable.Empty<TProduct>(), moneyReceived: 0m), 
				(state, current) => (
					state.leftProducts.Concat(Enumerable.Repeat(current.product, current.countProductsLeft)), 
					state.moneyReceived + current.moneyReceived
				)
			);
			
			return new CalculateState(calculateState.Money + calculatingResult.moneyReceived, 
				calculatingResult.leftProducts);
		}

		private (TProduct product, int countProductsLeft, decimal moneyReceived) TakeProduct(
			TProduct product,
			int countLeft,
			IEnumerable<ProductPackPrice> packedPrices) 
		{
			decimal moneyReceived = 0m;

			foreach(var price in packedPrices) {
				if (countLeft >= price.CountProducts) {
					// Use all available pack blocks.
					moneyReceived += (countLeft / price.CountProducts) * price.PriceForPack;
					countLeft = countLeft % price.CountProducts;
				}
			}
			
			return (product, countLeft, moneyReceived);
		}

		private IEnumerable<ProductPackPrice> GetPricesToCheckForProduct(TProduct product, IEnumerable<IGrouping<TProduct, Price>> pricingGroupedAroundProduct) {
			return GetSortedByProfitPackPrices(
				pricingGroupedAroundProduct.FirstOrDefault(priceItem => priceItem.Key == product) 
					?? Enumerable.Empty<Price>() 
			);
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
