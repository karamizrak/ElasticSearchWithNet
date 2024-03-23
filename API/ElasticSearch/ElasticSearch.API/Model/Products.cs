using ElasticSearch.API.Dto;

namespace ElasticSearch.API.Model
{
    public class Products
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price{ get; set; }
        public int Stock { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public ProductFeatures? Feature { get; set; }

        public ProductsDto? CreateProductDto()
        {
            return Feature == null ? new ProductsDto(Id,Name,Price,Stock,null) : new ProductsDto(Id, Name, Price, Stock, new ProductFeaturesDto(Feature.Width,Feature.Height,Feature.Color.ToString()));
        }

    }
}
