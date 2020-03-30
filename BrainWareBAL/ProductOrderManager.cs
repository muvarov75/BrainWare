using System;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace BrainWareBAL
{
    public class ProductOrderManager
    {
        BrainWareDAL.IProductOrders _dal;
        ILog _logger;

        public ProductOrderManager(BrainWareDAL.IProductOrders productOrders,ILog logger)
        {
            _dal = productOrders;
            _logger = logger;
        }
        public IList<Models.Order> GetOrdersByCompanyId(int companyId)
        {
            IList<Models.Order> orders = new List<Models.Order>();

            try
            {
                var productOrders = _dal.GetProductOrdersByCompanyId(companyId);
               
                if (productOrders != null)
                {
                    orders = (from ord in productOrders
                                  group ord by new { ord.CompanyId, ord.CompanyName, ord.OrderDescription, ord.OrderId } into g
                                  select new Models.Order() { CompanyId = g.Key.CompanyId, CompanyName = g.Key.CompanyName, Description = g.Key.OrderDescription, OrderId = g.Key.OrderId })
                                                 .ToList();

                    for (int i = 0; i < orders.Count(); i++)
                    {
                        orders[i].OrderProducts = (from ord in productOrders
                                                   where ord.OrderId == orders[i].OrderId
                                                   where ord.CompanyId == orders[i].CompanyId
                                                   select new Models.OrderProduct() { ProductId = ord.ProductId, OrderId = ord.OrderId, ProductName = ord.ProductName, Price = ord.OrderPrice, Quantity = ord.OrderQuantity })
                                        .ToList();

                        orders[i].OrderTotal = productOrders
                            .Where(c => c.OrderId == orders[i].OrderId)
                            .Where(c => c.CompanyId == orders[i].CompanyId)
                            .Sum(x => x.OrderPrice * x.OrderQuantity);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ProductOrderManager.GetOrdersByCompanyId()", ex);
            }

            return orders;

        }
    }
}
