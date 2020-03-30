using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrainWareBAL;
using System.Configuration;
using log4net;

namespace Web.Controllers
{
    using System.Web.Mvc;

    public class OrderController : ApiController
    {
        [HttpGet]
        public IList<BrainWareBAL.Models.Order> GetOrders(int id = -1)
        {
            IList<BrainWareBAL.Models.Order> orders =new List<BrainWareBAL.Models.Order>();
            ILog log = LogManager.GetLogger("logger");

            try
            {
                //inidiate dal
                var db = new BrainWareDAL.ProductOrders(log);
                db.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                var bal = new BrainWareBAL.ProductOrderManager(db, log);

                orders= bal.GetOrdersByCompanyId(id);
            }
            catch(Exception ex)
            {
                log.Error("OrderController.GetOrders()", ex);
            }

            return orders;

        }
    }
}
