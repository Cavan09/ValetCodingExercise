using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValetCodingExercise.Models;
using ValetCodingExercise.Controllers;

namespace ValetCoding.Tests
{
    [TestClass]
    public class TestSimpleProductController
    {
        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var result = controller.GetAllProducts() as List<Product>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }
    }
}