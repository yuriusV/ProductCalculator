using System.Collections.Generic;


namespace SaleTerminal
{
	public interface IPriceCalculator
	{
		CalculateState TakeSuitableProducts(
			IEnumerable<Price> pricing, CalculateState calculateState);

	}
}
