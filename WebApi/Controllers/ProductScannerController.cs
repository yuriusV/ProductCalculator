using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
	[Route("api/productScanner")]
	[ApiController]
	public class ProductScannerController : ControllerBase
	{
		private readonly IPointOfSaleTerminalService _pointOfSaleTerminalService;
		public ProductScannerController(IPointOfSaleTerminalService pointOfSaleTerminalService)
		{
			_pointOfSaleTerminalService = pointOfSaleTerminalService;
		}

		[Route("calculatePrice")]
		[HttpPost]
		public ActionResult CalculatePrice([FromBody] IEnumerable<string> products)
		{
			var price = _pointOfSaleTerminalService.CalculatePrice(products);
			if (price == null)
			{
				return new JsonResult(new { Success = false, ErrorMessage = "Not all products exists in price list", Data = 0m });
			}
			return new JsonResult(new { Success = true, ErrorMessage = "", Data = price });
		}
	}
}
