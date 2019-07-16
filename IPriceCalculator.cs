using System.Collections.Generic;


namespace SaleTerminal
{
	public interface IPriceCalculator {
		CalculateState TakeAndCalculate(
			IEnumerable<Price> pricing, CalculateState calculateState);

	}
}
