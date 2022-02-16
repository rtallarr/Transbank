using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;
using System.Text.Json;
using Transbank.Responses.CommonResponses;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadKeys : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                Task<LoadKeysResponse> response = POSAutoservicio.Instance.LoadKeys();
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
