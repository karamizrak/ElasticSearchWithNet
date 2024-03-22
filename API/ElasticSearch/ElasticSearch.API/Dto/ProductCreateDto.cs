using ElasticSearch.API.Model;

namespace ElasticSearch.API.Dto
{
    public record ProductCreateDto(string Name, decimal Price, int Stock, ProductFeatureDto Feature)
    {
        public Product CreateProduct()
        {
            return new Product
            {
                Name = Name, Price = Price, Stock = Stock,
                Feature = new ProductFeature
                    { Height = Feature.Height, Width = Feature.Width, Color = (EColor)int.Parse(Feature.Color)}
            };
        }
    }

}
