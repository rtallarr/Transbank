using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;
using Transbank.Responses.AutoservicioResponse;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitResponse : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                Task<InitializationResponse> response = POSAutoservicio.Instance.InitializationResponse();
                response.Wait();
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
