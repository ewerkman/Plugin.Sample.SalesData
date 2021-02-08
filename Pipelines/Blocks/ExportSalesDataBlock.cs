using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Sample.SalesData.Policies;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Framework.Pipelines;

namespace Plugin.Sample.SalesData.Pipelines.Blocks
{
    public class ExportSalesDataBlock : AsyncPipelineBlock<Order, Order, CommercePipelineExecutionContext>
    {
        public override Task<Order> RunAsync(Order arg, CommercePipelineExecutionContext context)
        {
            var salesDataPolicy = context.GetPolicy<SalesDataPolicy>();
            
            arg.Lines.ToList().ForEach( async l =>
            {
                string queryString = "INSERT INTO [dbo].[Sales]([ProductID],[VariantID],[Quantity],[OrderDate],[OrderId]) VALUES (@ProductId, @VariantId, @Quantity, @OrderDate, @OrderId)";
                using (SqlConnection connection = new SqlConnection(salesDataPolicy.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    var itemIdParts = l.ItemId.Split('|');
                    var productIdParam = new SqlParameter("@ProductId", SqlDbType.NVarChar);
                    productIdParam.Value = itemIdParts[1];
                    command.Parameters.Add(productIdParam);
                    
                    var variantIdParam = new SqlParameter("@VariantId", SqlDbType.NVarChar);
                    variantIdParam.Value = itemIdParts[2];
                    command.Parameters.Add(variantIdParam);

                    var quantityParam = new SqlParameter("@Quantity", SqlDbType.Int);
                    quantityParam.Value = l.Quantity;
                    command.Parameters.Add(quantityParam);

                    var OrderDateParam = new SqlParameter("@OrderDate", SqlDbType.DateTime);
                    OrderDateParam.Value = arg.OrderPlacedDate.DateTime;
                    command.Parameters.Add(OrderDateParam);

                    var orderIdParam = new SqlParameter("@OrderId", SqlDbType.NVarChar);
                    orderIdParam.Value = arg.OrderConfirmationId;
                    command.Parameters.Add(orderIdParam);

                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                }
            });

            return Task.FromResult(arg);
        }
    }
}