using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainWareBAL.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public decimal OrderTotal { get; set; }
        public IList<OrderProduct> OrderProducts { get; set; }
    }
}
