using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Configurations
{
    public static class ConfigRegistration
    {
        public static IServiceCollection AddConfigs(this IServiceCollection services,
            IConfiguration configurationSection)
        {


            services.AddOptions<BrokerConfig>()
                      .Configure(brokerConfig =>
                      {
                          brokerConfig.Address = "*";
                          brokerConfig.Port = 5051;
                      })
                      .Bind(configurationSection.GetSection(nameof(BrokerConfig)));


            return services;
        }
    }
}
