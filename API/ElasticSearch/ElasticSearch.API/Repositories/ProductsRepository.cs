using Elastic.Clients.Elasticsearch;
using ElasticSearch.API.Dto;
using ElasticSearch.API.Model;

using System.Collections.Immutable;

namespace ElasticSearch.API.Repositories
{
    public class ProductsRepository
    {
        private readonly ElasticsearchClient _client;
        private const string IndexName = "products";

        public ProductsRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<Products?> SaveAsync(Products product)
        {
            product.Created = DateTime.Now;

            var response = await _client.IndexAsync(product, x => x.Index(IndexName).Id(Guid.NewGuid().ToString()));
            //fast fail uygun değil ise hemen dönüş yap.
            if (!response.IsSuccess())
                return null;

            product.Id = response.Id;
            return product;
        }

        public async Task<ImmutableList<Products>> GetAllAsync()
        {
            var result=await _client.SearchAsync<Products>(s=>
                s.Index(IndexName)
                    .Query(q=> 
                        q.MatchAll()
                        )
                );
            foreach (var item in result.Hits) item.Source.Id=item.Id;
            
                return result.Documents.ToImmutableList();
        }

        public async Task<Products?> GetByIdAsync(string id)
        {
            var result = await _client.GetAsync<Products>( id, s =>
                s.Index(IndexName));
            if(!result.IsSuccess() ) return null;


            result.Source.Id = result.Id;
            return result.Source;
        }

        public async Task<bool> UpdateAsync(ProductsUpdateDto dto)
        {
            var result = await _client.UpdateAsync<Products, ProductsUpdateDto>(IndexName,dto.Id,x=>x.Doc(dto));
            return result.IsSuccess();
        }
        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var result = await _client.DeleteAsync<Products>(id , x => x.Index(IndexName));
            return result;
        }
    }
}
