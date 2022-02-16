using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitComm : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                Task<bool> initializationResult = POSAutoservicio.Instance.Initialization();
                initializationResult.Wait();
                if (initializationResult.Result)
                    return Ok("Pos Initialized");
                else
                    return Ok("Pos NOT Initialized");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
