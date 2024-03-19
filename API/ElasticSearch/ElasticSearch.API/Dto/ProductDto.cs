using ElasticSearch.API.Model;
using Nest;

namespace ElasticSearch.API.Dto
{
    public record ProductDto(string Id, string Name, decimal Price, int Stock, ProductFeatureDto? Feature)
    { }
}
