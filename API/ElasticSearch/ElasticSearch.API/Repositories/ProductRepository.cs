using ElasticSearch.API.Dto;
using ElasticSearch.API.Model;
using Nest;
using System.Collections.Immutable;

namespace ElasticSearch.API.Repositories
{
    public class ProductRepository
    {
        private readonly ElasticClient _client;
        private const string IndexName = "products11";

        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task<Product?> SaveAsync(Product product)
        {
            product.Created = DateTime.Now;

            var response = await _client.IndexAsync(product, x => x.Index(IndexName).Id(Guid.NewGuid().ToString()));
            //fast fail uygun değil ise hemen dönüş yap.
            if (!response.IsValid)
                return null;

            product.Id = response.Id;
            return product;
        }

        public async Task<ImmutableList<Product>> GetAllAsync()
        {
            var result=await _client.SearchAsync<Product>(s=>
                s.Index(IndexName)
                    .Query(q=> 
                        q.MatchAll()
                        )
                );
            foreach (var item in result.Hits) item.Source.Id=item.Id;
            
                return result.Documents.ToImmutableList();
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            var result = await _client.GetAsync<Product>( id, s =>
                s.Index(IndexName));
            if(!result.IsValid ) return null;


            result.Source.Id = result.Id;
            return result.Source;
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto dto)
        {
            var result = await _client.UpdateAsync<Product, ProductUpdateDto>(dto.Id, x => x.Index(IndexName).Doc(dto));
            return result.IsValid;
        }
        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var result = await _client.DeleteAsync<Product>(id , x => x.Index(IndexName));
            return result;
        }
    }
}
