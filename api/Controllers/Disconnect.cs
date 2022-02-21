using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Transbank.POSAutoservicio;

namespace api.Controllers
{
    class DisconnectResponse
    {
        public bool Success { get; set; }
        public string? Details { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class Disconnect : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            var DisconnectResponse = new DisconnectResponse();
            try
            {
                Task<bool> pollResult = POSAutoservicio.Instance.Poll();
                pollResult.Wait();

                DisconnectResponse.Success = pollResult.Result;

                if (pollResult.Result)
                {
                    POSAutoservicio.Instance.ClosePort();
                    DisconnectResponse.Details = "Port closed successfully";
                    string jsonStrings = JsonSerializer.Serialize(DisconnectResponse);
                    return Ok(jsonStrings);
                } else
                {
                    DisconnectResponse.Details = "The port is closed";
                    string jsonString = JsonSerializer.Serialize(DisconnectResponse);
                    return BadRequest(jsonString);

                }
            } catch (Exception ex)
            {
                DisconnectResponse.Success = false;
                DisconnectResponse.Details = ex.Message;
                string jsonString = JsonSerializer.Serialize(DisconnectResponse);
                return BadRequest(jsonString);
            }
        }
    }
}
