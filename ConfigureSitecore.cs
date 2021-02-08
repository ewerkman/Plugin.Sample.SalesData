using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Sample.SalesData.Pipelines.Blocks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Commerce.Plugin.Promotions;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;

namespace Plugin.Sample.SalesData
{
    public class ConfigureSitecore : IConfigureSitecore
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            services.Sitecore().Pipelines(
                config =>
                    config
                        .ConfigurePipeline<IPendingOrdersMinionPipeline>( c => c
                            .Add<ExportSalesDataBlock>().After<ValidatePendingOrderBlock>())
            );
        }
    }
}