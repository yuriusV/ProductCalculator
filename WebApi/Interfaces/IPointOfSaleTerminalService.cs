namespace WebApi.Interfaces 
{
	using System.Collections.Generic;
	public interface IPointOfSaleTerminalService
	{
		decimal? CalculatePrice(IEnumerable<string> input);
	}

}