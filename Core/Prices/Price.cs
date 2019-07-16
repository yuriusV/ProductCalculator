using TProduct = System.String;


namespace SaleTerminal
{
	public abstract class Price
	{

		public TProduct Product { get; private set; }
		public Price(TProduct productName)
		{
			Product = productName;
		}
	}
}
