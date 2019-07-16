using System.Collections.Generic;

using TPriceMeasure = System.Double;
using TProduct = System.String;


namespace SaleTerminal
{
	public class CalculateState
	{
		public TPriceMeasure Money { get; private set; }
		public IEnumerable<TProduct> LeftProducts { get; private set; }

		public CalculateState(TPriceMeasure money, IEnumerable<TProduct> leftProducts)
		{
			Money = money;
			LeftProducts = leftProducts;
		}
	}
}
