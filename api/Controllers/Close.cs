using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;
using System.Text.Json;
using Transbank.Responses.AutoservicioResponse;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Close : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                Task<CloseResponse> response = POSAutoservicio.Instance.Close(true);
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
