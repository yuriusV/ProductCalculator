
using TPriceMeasure = System.Double;
using TProduct = System.String;


namespace SaleTerminal
{
	public class ProductPackPrice: Price {
		public int CountProducts { get; private set; }
		public TPriceMeasure PriceForPack { get; private set; }
		public ProductPackPrice(TProduct productName, int countProducts, TPriceMeasure priceForPack): base(productName)
		{
			CountProducts = countProducts;
			PriceForPack = priceForPack;
		}
	}
}
