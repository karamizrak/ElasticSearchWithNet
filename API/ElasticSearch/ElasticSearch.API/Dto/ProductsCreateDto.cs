using ElasticSearch.API.Model;

namespace ElasticSearch.API.Dto
{
    public record ProductsCreateDto(string Name, decimal Price, int Stock, ProductFeaturesDto Feature)
    {
        public Products CreateProduct()
        {
            return new Products
            {
                Name = Name, Price = Price, Stock = Stock,
                Feature = new ProductFeatures
                    { Height = Feature.Height, Width = Feature.Width, Color = (EColor)int.Parse(Feature.Color)}
            };
        }
    }

}
