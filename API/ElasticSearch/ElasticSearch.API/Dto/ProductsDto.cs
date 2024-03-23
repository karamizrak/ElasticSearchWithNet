namespace ElasticSearch.API.Dto
{
    public record ProductsDto(string Id, string Name, decimal Price, int Stock, ProductFeaturesDto? Feature)
    { }
}
