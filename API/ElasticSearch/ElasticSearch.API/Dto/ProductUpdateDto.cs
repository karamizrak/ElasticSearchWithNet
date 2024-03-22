using ElasticSearch.API.Model;

namespace ElasticSearch.API.Dto
{
    public record ProductUpdateDto(string Id,string Name, decimal Price, int Stock, ProductFeatureDto Feature)
    {
     
    }

}
