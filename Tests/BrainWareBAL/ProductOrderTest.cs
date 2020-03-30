using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using log4net;

namespace Tests.Controllers
{
    [TestClass]
    public class ProductOrderTest
    {

        [TestMethod]
        public void NoOrdersFoundTest()
        {
            var o = new Mock<BrainWareDAL.IProductOrders>();
            o.Setup(s => s.GetProductOrdersByCompanyId(0)).Returns(new List<BrainWareDAL.Models.ProductOrder>());

            BrainWareDAL.IProductOrders db = o.Object;

            var bal = new BrainWareBAL.ProductOrderManager(db,LogManager.GetLogger("logger"));

            var orders= bal.GetOrdersByCompanyId(1);

            Assert.IsTrue(orders.Count == 0);

        }

        [TestMethod]
        public void CorrectOrderTotalAndOrderCountTest()
        {
            var o = new Mock<BrainWareDAL.IProductOrders>();

            var list = new List<BrainWareDAL.Models.ProductOrder>();

            list.Add(new BrainWareDAL.Models.ProductOrder() {
                CompanyId=1,
                CompanyName="abc",
                OrderDescription="test order",
                OrderId=1,
                OrderPrice=0.11M,
                OrderQuantity=5,
                ProductId=1,
                ProductName="test product",
                ProductPrice=1 });

            list.Add(new BrainWareDAL.Models.ProductOrder()
            {
                CompanyId = 1,
                CompanyName = "abc",
                OrderDescription = "test order",
                OrderId = 1,
                OrderPrice = 0.11M,
                OrderQuantity = 5,
                ProductId = 2,
                ProductName = "test product",
                ProductPrice = 2
            });

            list.Add(new BrainWareDAL.Models.ProductOrder()
            {
                CompanyId = 1,
                CompanyName = "abc",
                OrderDescription = "test order",
                OrderId = 2,
                OrderPrice = 0.11M,
                OrderQuantity = 5,
                ProductId = 2,
                ProductName = "test product",
                ProductPrice = 2
            });

            o.Setup(s => s.GetProductOrdersByCompanyId(1)).Returns(list);

            BrainWareDAL.IProductOrders db = o.Object;

            var bal = new BrainWareBAL.ProductOrderManager(db,LogManager.GetLogger("logger"));

            var orders = bal.GetOrdersByCompanyId(1);

            Assert.IsTrue(orders.Count == 2);
            Assert.IsTrue(orders[0].OrderTotal.CompareTo(1.1M) == 0);
            Assert.IsTrue(orders[1].OrderTotal.CompareTo(0.55M) == 0);
        }

    }
}
