using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using log4net;

namespace BrainWareDAL
{
    public class ProductOrders: IProductOrders
    {
        ILog _logger;
        public ProductOrders(ILog logger)
        {
            _logger = logger;
        }
        public string ConnectionString { get; set; }
        public IList<Models.ProductOrder> GetProductOrdersByCompanyId(int companyId)
        {
            IList<Models.ProductOrder> porders = new List<Models.ProductOrder>();

            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(ConnectionString))
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand("select c.company_id as CompanyId,c.name as CompanyName,o.order_id as OrderId,o.description as OrderDescription,op.product_id as ProductId,p.name as ProductName,op.quantity as OrderQuantity,p.price as ProductPrice,op.price as OrderPrice from company c inner join [order] o on c.company_id = o.company_id inner join orderproduct op on op.order_id = o.order_id inner join product p on p.product_id = op.product_id where c.company_id = @CompanyId",
                        sqlconnection);

                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@CompanyId";
                    param.Value = companyId;

                    cmd.Parameters.Add(param);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Models.ProductOrder po;

                    while (reader.Read())
                    {
                        po = new Models.ProductOrder();

                        po.CompanyName = reader.SafeGetString("CompanyName");
                        po.OrderDescription = reader.SafeGetString("OrderDescription");
                        po.ProductName = reader.SafeGetString("ProductName");
                        po.OrderId = (int)reader["OrderId"];
                        po.OrderPrice = (decimal)reader["OrderPrice"];
                        po.OrderQuantity = (int)reader["OrderQuantity"];
                        po.ProductId = (int)reader["ProductId"];
                        po.ProductPrice = (decimal)reader["ProductPrice"];
                        po.CompanyId = (int)reader["CompanyId"];

                        porders.Add(po);

                    }

                    sqlconnection.Close();

                }
            }
            catch (Exception ex)
            {
                _logger.Error("BrainWareDAL.GetProductOrdersByCompanyId()", ex);
            }
            return porders;
        }
    }
}
