namespace WebApi.Interfaces {
	using System.Collections.Generic;
	using SaleTerminal;
	public interface IPricesRepository
	{
		IEnumerable<Price> GetPrices();
	}
}