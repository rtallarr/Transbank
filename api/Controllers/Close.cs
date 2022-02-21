using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;
using System.Text.Json;
using Transbank.Responses.AutoservicioResponse;

namespace api.Controllers
{
    class OutClose
    {
        public bool Success { get; set; }
        public InClose? Details { get; set; }
    }
    class InClose
    {
        public List<string>? PrintingField { get; set; }
        public long CommerceCode { get; set; }
        public string? TerminalId { get; set; }
        public string? Response { get; set; }
        public int FunctionCode { get; set; }
        public string? ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
        public bool Success { get; set; }

    }
    class OutCloseEx
    {
        public bool Success { get; set; }
        public string? Details { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class Close : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                var OutClose = new OutClose();
                Task<CloseResponse> response = POSAutoservicio.Instance.Close(true);
                response.Wait();

                string temp = JsonSerializer.Serialize(response.Result);
                InClose? InJson = JsonSerializer.Deserialize<InClose>(temp);

                OutClose.Success = true;
                OutClose.Details = InJson;

                string jsonString = JsonSerializer.Serialize(OutClose);
                return Ok(jsonString);
            } catch (Exception ex) 
            {
                var CloseJsonEx = new OutCloseEx();
                CloseJsonEx.Success = false;
                CloseJsonEx.Details = JsonSerializer.Serialize(ex.Message);
                string jsonString = JsonSerializer.Serialize(CloseJsonEx);
                return BadRequest(jsonString);
            }
        }
    }
}
