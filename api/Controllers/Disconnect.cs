using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Disconnect : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                Task<bool> pollResult = POSAutoservicio.Instance.Poll();
                pollResult.Wait();
                if (pollResult.Result)
                {
                    POSAutoservicio.Instance.ClosePort();
                    return Ok("POS Disconnected");
                }
                return BadRequest("The port is closed");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
