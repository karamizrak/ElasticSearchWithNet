using ElasticSearch.API.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        [NonAction]
        public IActionResult CreateActionResult<T>(ResponseDto<T> response)
        {
            var retVal = new ObjectResult(null)
            {
                StatusCode = response.HttpStatusCode.GetHashCode()
            };

            if (response.HttpStatusCode != System.Net.HttpStatusCode.NoContent)
                retVal.Value = response;

            return retVal;
        }
    }
}
