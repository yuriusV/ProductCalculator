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
		public ProductScannerController(IPointOfSaleTerminalService pointOfSaleTerminalService) {
			_pointOfSaleTerminalService = pointOfSaleTerminalService;
		}

		[Route("calculatePrice")]
        [HttpPost]
        public ActionResult CalculatePrice([FromBody] IEnumerable<string> products)
        {
			return new JsonResult(_pointOfSaleTerminalService.CalculatePrice(products));
        }
    }
}
