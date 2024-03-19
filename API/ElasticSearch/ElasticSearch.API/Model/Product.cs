using ElasticSearch.API.Dto;
using Nest;

namespace ElasticSearch.API.Model
{
    public class Product
    {
        [PropertyName("_id")] public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price{ get; set; }
        public int Stock { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public ProductFeature? Feature { get; set; }

        public ProductDto? CreateProductDto()
        {
            return Feature == null ? new ProductDto(Id,Name,Price,Stock,null) : new ProductDto(Id, Name, Price, Stock, new ProductFeatureDto(Feature.Width,Feature.Height,Feature.Color));
        }

    }
}
