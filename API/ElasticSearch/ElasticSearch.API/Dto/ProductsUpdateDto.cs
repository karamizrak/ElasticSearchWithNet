using ElasticSearch.API.Model;

namespace ElasticSearch.API.Dto
{
    public record ProductsUpdateDto(string Id,string Name, decimal Price, int Stock, ProductFeaturesDto Feature)
    {
     
    }

}
