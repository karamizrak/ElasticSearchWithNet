using ElasticSearch.API.Model;

namespace ElasticSearch.API.Dto
{
    public record ProductCreateDto(string name, decimal price, int stock, ProductFeatureDto FeatureDto)
    {
        public Product CreateProduct()
        {
            return new Product
            {
                Name = name, Price = price, Stock = stock,
                Feature = new ProductFeature
                    { Height = FeatureDto.height, Width = FeatureDto.with, Color = FeatureDto.color }
            };
        }
    }

}
