using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;
using Transbank.Responses.AutoservicioResponse;
using System.Text.Json;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LastSaleController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                Task<LastSaleResponse> response = POSAutoservicio.Instance.LastSale(true);
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
