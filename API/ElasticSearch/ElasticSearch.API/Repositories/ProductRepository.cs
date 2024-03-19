using ElasticSearch.API.Model;
using Nest;

namespace ElasticSearch.API.Repositories
{
    public class ProductRepository
    {
        private readonly ElasticClient _client;

        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task<Product?> SaveAsync(Product product)
        {
            product.Created = DateTime.Now;

            var response = await _client.IndexAsync(product, x => x.Index("products"));
            //fast fail uygun değil ise hemen dönüş yap.
            if (!response.IsValid)
                return null;

            product.Id = response.Id;
            return product;
        }
    }
}
