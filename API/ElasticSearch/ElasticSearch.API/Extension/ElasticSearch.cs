using Elasticsearch.Net;
using Nest;

namespace ElasticSearch.API.Extension
{
    public static class ElasticSearch
    {
        public static void AddElastic(this IServiceCollection services,IConfiguration configuration)
        {

            var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));
            var settings = new ConnectionSettings(pool);
            //Bu kısım daha sonra kullanılabilir.
            //settings.BasicAuthentication("username","password")
            var client = new ElasticClient(settings);

            services.AddSingleton(client);
        }
    }
}
