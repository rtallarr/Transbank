using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Disconnect : ControllerBase
    {
        [HttpPost(Name = "Disconnect")]
        public IActionResult Post()
        {
            try
            {
                POSAutoservicio.Instance.ClosePort();
                //Console.WriteLine(POSAutoservicio.Instance.ClosePort());
                return Ok("POS Disconnected");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
