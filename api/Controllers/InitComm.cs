using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Transbank.POSAutoservicio;

namespace api.Controllers
{
    class OutInitComm
    {
        public bool Success { get; set; }
        public string? Details { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class InitComm : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            var OutInitComm = new OutInitComm();
            try
            {
                Task<bool> initializationResult = POSAutoservicio.Instance.Initialization();
                initializationResult.Wait();
                OutInitComm.Success = initializationResult.Result;
                if (initializationResult.Result)
                {
                    OutInitComm.Details = "Pos Initialized";
                    string jsonString = JsonSerializer.Serialize(OutInitComm);
                    return Ok(jsonString);
                }
                else
                {
                    OutInitComm.Details = "Pos NOT Initialized";
                    string jsonString = JsonSerializer.Serialize(OutInitComm);
                    return BadRequest(jsonString);
                }    
            }
            catch (Exception ex)
            {
                OutInitComm.Success = false;
                OutInitComm.Details = ex.Message;
                string jsonString = JsonSerializer.Serialize(OutInitComm);
                return BadRequest(jsonString);
            }
        }
    }
}
