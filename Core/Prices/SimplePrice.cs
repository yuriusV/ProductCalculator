
using TPriceMeasure = System.Double;
using TProduct = System.String;


namespace SaleTerminal
{
	public class SimplePrice : Price
	{

		public TPriceMeasure Price { get; private set; }
		public SimplePrice(TProduct productName, TPriceMeasure price) : base(productName)
		{
			Price = price;
		}
	}
}
