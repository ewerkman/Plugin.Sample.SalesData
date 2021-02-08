using Sitecore.Commerce.Core;

namespace Plugin.Sample.SalesData.Policies
{
    public class SalesDataPolicy : Policy
    {
        public string ConnectionString { get; set; } = "Server=.;Database=SalesData;Trusted_Connection=True;";
    }
}