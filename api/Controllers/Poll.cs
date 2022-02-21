using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Transbank.POSAutoservicio;

namespace api.Controllers
{
    class PollResponse
    {
        public bool Success { get; set; }
        public string? Details { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class Poll : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            var PollResponse = new PollResponse();
            try
            {
                Task<bool> pollResult = POSAutoservicio.Instance.Poll();
                pollResult.Wait();
                PollResponse.Success = pollResult.Result;
                if (pollResult.Result)
                {
                    PollResponse.Details = "Pos Connected";
                    string jsonString = JsonSerializer.Serialize(PollResponse);
                    return Ok(jsonString);
                }
                else
                {
                    PollResponse.Details = "Pos NOT Connected";
                    string jsonString = JsonSerializer.Serialize(PollResponse);
                    return BadRequest(jsonString);
                }
            } catch (Exception ex)
            {
                PollResponse.Success = false;
                PollResponse.Details = ex.Message;
                string jsonString = JsonSerializer.Serialize(PollResponse);
                return BadRequest(jsonString);
            }
        }
    }
}
