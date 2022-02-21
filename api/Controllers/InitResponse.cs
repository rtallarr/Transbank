using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Transbank.POSAutoservicio;
using Transbank.Responses.AutoservicioResponse;

namespace api.Controllers
{
    class OutInitResponse
    {
        public bool Success { get; set; }
        public InInitResponse? Details { get; set; }
    }
    class InInitResponse
    {
        public int FunctionCode { get; set; }
        public int ResponseCode { get; set; }
        public string? ResponseMessage { get; set; }
        public bool Success { get; set; }
        public DateTime RealDate { get; set; }
    }
    class OutInitResponseEx
    {
        public bool Success { get; set; }
        public string? Details { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class InitResponse : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                var OutInitResponse = new OutInitResponse();
                Task<InitializationResponse> response = POSAutoservicio.Instance.InitializationResponse();
                response.Wait();

                string temp = JsonSerializer.Serialize(response.Result);
                InInitResponse? InJson = JsonSerializer.Deserialize<InInitResponse>(temp);

                OutInitResponse.Success = true;
                OutInitResponse.Details = InJson;

                string jsonString = JsonSerializer.Serialize(OutInitResponse);
                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                var OutInitResponseEx = new OutInitResponseEx();
                OutInitResponseEx.Success = false;
                OutInitResponseEx.Details = ex.Message;
                string jsonString = JsonSerializer.Serialize(OutInitResponseEx);
                return BadRequest(jsonString);
            }
        }
    }
}