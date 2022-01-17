using Assessment.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Stock.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected GenericResult result { get; }
        public BaseController()
        {
            result = new GenericResult();
        }
    }
}
