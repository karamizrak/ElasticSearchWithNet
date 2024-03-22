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
        private readonly ILogger<ProductService> logger;
        public ProductService(ProductRepository productRepository, ILogger<ProductService> logger)
        {
            this.productRepository = productRepository;
            this.logger = logger;
        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        {
            var response = await this.productRepository.SaveAsync(request.CreateProduct());

            if (response == null)
            {
                return ResponseDto<ProductDto>.Fail("Kayıt sırasında bir hata meydana geldi", HttpStatusCode.InternalServerError);
            }

            return ResponseDto<ProductDto>.Success(response?.CreateProductDto(), HttpStatusCode.Created);


        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var products = await productRepository.GetAllAsync();
            var retVal = new List<ProductDto>();
            //var retVal= products.Select(s => new ProductDto(s.Id,s.Name,s.Price,s.Stock,new ProductFeatureDto(s.Feature.Width,s.Feature.Height,s.Feature.Color)
            //)).ToList();

            foreach (var x in products)
            {
                if (x.Feature is null)
                {
                    retVal.Add(new ProductDto(x.Id, x.Name, x.Price, x.Stock, null));
                    continue;
                }

                retVal.Add(new ProductDto(x.Id, x.Name, x.Price, x.Stock, new ProductFeatureDto(x.Feature.Width, x.Feature.Height, x.Feature.Color.ToString())));
            }




            return ResponseDto<List<ProductDto>>.Success(retVal, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
        {
            var p = await productRepository.GetByIdAsync(id);

            if (p == null)
                return ResponseDto<ProductDto>.Fail("Ürün bulunamadı", HttpStatusCode.NotFound);

            return ResponseDto<ProductDto>.Success(p.CreateProductDto(), HttpStatusCode.OK);

        }

        public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto dto)
        {
            var p = await productRepository.UpdateAsync(dto);

            return !p ? ResponseDto<bool>.Fail("Güncelleme sırasında hata oluştu", HttpStatusCode.NotFound) : ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }

        public async Task<ResponseDto<bool>> DeleteAsync(string id)
        {
            var p = await productRepository.DeleteAsync(id);
            if (!p.IsValid && p.Result == Nest.Result.NotFound)
            {
                return ResponseDto<bool>.Fail("Silinmeye çalışılan veri yok.", HttpStatusCode.NotFound);
            }

            if (!p.IsValid)
            {
                logger.LogError(p.OriginalException, p.ServerError.Error.ToString());

                return ResponseDto<bool>.Fail("Silme sırasında hata oluştu", HttpStatusCode.InternalServerError);
            }



            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }
    }
}
