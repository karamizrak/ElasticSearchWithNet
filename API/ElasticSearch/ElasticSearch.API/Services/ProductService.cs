using ElasticSearch.API.Dto;
using ElasticSearch.API.Model;
using ElasticSearch.API.Repositories;
using System.Collections.Immutable;
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

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var products = await productRepository.GetAllAsync();
            var retVal= new List<ProductDto>();
            //var retVal= products.Select(s => new ProductDto(s.Id,s.Name,s.Price,s.Stock,new ProductFeatureDto(s.Feature.Width,s.Feature.Height,s.Feature.Color)
            //)).ToList();

            foreach (var x in products)
            {
                if (x.Feature is null)
                    retVal.Add(new ProductDto(x.Id, x.Name, x.Price, x.Stock, null));
                else
                    retVal.Add(new ProductDto(x.Id, x.Name, x.Price, x.Stock, new ProductFeatureDto(x.Feature.Width, x.Feature.Height, x.Feature.Color)));
            }




            return ResponseDto<List<ProductDto>>.Success(retVal,HttpStatusCode.OK);
        }
    }
}
