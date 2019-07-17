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
			Assert.AreEqual(_terminal.CalculateTotal(), 0);
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

			Assert.AreEqual(_terminal.CalculateTotal(), productPrice);
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
			_terminal.Scan(productName);
			_terminal.Scan(productName);

			Assert.AreEqual(_terminal.CalculateTotal(), productPrice);
		}

		[Test]
		public void ReturnsNullForNotExistingProducts()
		{
			string notExistingProduct = "Strange";
			string existingProduct = "A";
			decimal price = 1m;

			_terminal.SetPricing(new List<Price>() {
				new SimplePrice(existingProduct, price)
			});

			_terminal.Scan(notExistingProduct);

			Assert.IsNull(_terminal.CalculateTotal());
		}

		[Test]
		public void ReturnsNullForNotExistingProductsInCombinationWithExisting()
		{
			string notExistingProduct = "Strange";
			string existingProduct = "A";
			decimal price = 1m;

			_terminal.SetPricing(new List<Price>() {
				new SimplePrice(existingProduct, price)
			});

			_terminal.Scan(existingProduct);
			_terminal.Scan(notExistingProduct);

			Assert.IsNull(_terminal.CalculateTotal());
		}

		[Test]
		public void Combination1IsCalculatingCorrect()
		{
			var pricing = GetSimplePricelist();

			_terminal.SetPricing(pricing);

			ScanProducts(_terminal, "A", "B", "C", "D", "A", "B", "A");
			Assert.AreEqual(_terminal.CalculateTotal(), 13.25m);
		}

		[Test]
		public void Combination2IsCalculatingCorrect()
		{
			var pricing = GetSimplePricelist();

			_terminal.SetPricing(pricing);

			ScanProducts(_terminal, "C", "C", "C", "C", "C", "C", "C");
			Assert.AreEqual(_terminal.CalculateTotal(), 6m);
		}

		[Test]
		public void TestThatInstanceCanCorrectProcesTwoSequntialCalculatings()
		{
			Combination1IsCalculatingCorrect();
			Combination2IsCalculatingCorrect();
		}

		// Just reduce syntax to create test data.
		private void ScanProducts(PointOfSaleTerminal terminal, params string[] products)
		{
			foreach (var product in products)
			{
				terminal.Scan(product);
			}
		}

		private IEnumerable<Price> GetSimplePricelist()
		{
			return new List<Price>() {
				new SimplePrice("A", 1.25m),
				new ProductPackPrice("A", 3, 3m),
				new SimplePrice("B", 4.25m),
				new SimplePrice("C", 1m),
				new ProductPackPrice("C", 6, 5m),
				new SimplePrice("D", 0.75m)
			};
		}
	}
}