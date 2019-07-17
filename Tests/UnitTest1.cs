using NUnit.Framework;
using SaleTerminal;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

namespace Tests
{
    public class PointOfSaleTerminalTest
    {
		private PointOfSaleTerminal _terminal;

        [SetUp]
        public void Setup()
        {
			_terminal = new PointOfSaleTerminal();
        }

        [Test]
        public void EmptyIsCalculatedToZero()
        {
			_terminal.SetPricing(Enumerable.Empty<Price>());
            Assert.That(_terminal.CalculateTotal() == 0);
        }

		[Test]
        public void OneProductIsCalculatedCorrect()
        {
			string productName = "A";
			decimal productPrice = 1.2m;

			_terminal.SetPricing(new List<Price> {
				new SimplePrice(productName, productPrice)
			});
			_terminal.Scan(productName);

            Assert.That(_terminal.CalculateTotal() == productPrice);
        }

		[Test]
        public void ProductPackIsCalculatedCorrect()
        {
			string productName = "A";
			int countProducts = 3;
			decimal productPrice = 1.2m;

			_terminal.SetPricing(new List<Price> {
				new ProductPackPrice(productName, countProducts, productPrice)
			});
			_terminal.Scan(productName);

            Assert.That(_terminal.CalculateTotal() == productPrice);
        }
    }
}