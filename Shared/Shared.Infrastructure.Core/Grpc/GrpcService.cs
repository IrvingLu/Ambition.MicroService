using Consul;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Shared.Infrastructure.Core.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Grpc
{
    public class GrpcService
    {
        private readonly IConfiguration configuration;
        public GrpcService(IConfiguration Configuration)
        {
            this.configuration = Configuration;
        }
        //通过调用Consul查询对应的服务地址，方便gRPC调用
        public async Task<GrpcChannel> GetChannelByNameAsync(string name)
        {
            name += "Grpc";
            var consulClient = new ConsulClient(x => x.Address = new Uri(configuration["ConsulAddress"]));
            var services = await consulClient.Catalog.Service(name);
            if (services.Response.Length == 0)
            {
                throw new InternalException($"未发现服务 {name}");
            }
            var service = services.Response[0];
            var address = $"http://{service.ServiceAddress}:{service.ServicePort}";
            return GrpcChannel.ForAddress(address);
        }
    }
}
