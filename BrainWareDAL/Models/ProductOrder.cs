using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainWareDAL.Models
{
    public class ProductOrder
    {
        public string CompanyName { get; set; }
        public string OrderDescription { get; set; }
        public string ProductName { get; set; }
        public int CompanyId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal OrderPrice { get; set; }
    }
}
