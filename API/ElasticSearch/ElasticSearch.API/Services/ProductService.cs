using ElasticSearch.API.Dto;
using ElasticSearch.API.Model;
using ElasticSearch.API.Repositories;
using System.Net;

namespace ElasticSearch.API.Services
{
    public class ProductService
    {
        private readonly ProductRepository productRepository;
        public ProductService(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        {
            var response=await this.productRepository.SaveAsync(request.CreateProduct());
            
            if (response == null)
            {
                return ResponseDto<ProductDto>.Fail("Kayıt sırasında bir hata meydana geldi",HttpStatusCode.InternalServerError);
            }

            return ResponseDto<ProductDto>.Success(response?.CreateProductDto(),HttpStatusCode.Created);

            
        }
    }
}
