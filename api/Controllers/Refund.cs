using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;
using System.Text.Json;
using Transbank.Responses.CommonResponses;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Refund : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                Task<RefundResponse> response = POSAutoservicio.Instance.Refund();
                response.Wait();
                string jsonString = JsonSerializer.Serialize(response.Result);
                return Ok(jsonString);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
