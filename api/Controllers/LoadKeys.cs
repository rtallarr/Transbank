using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;
using System.Text.Json;
using Transbank.Responses.CommonResponses;

namespace api.Controllers
{
    class OutLoadKeys
    {
        public bool Success { get; set; }
        public InLoadKeys? Details { get; set; }
    }
    class InLoadKeys
    {
        public long CommerceCode { get; set; }
        public string? TerminalId { get; set; }
        public string? Response { get; set; }
        public int FunctionCode { get; set; }
        public string? ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
        public bool Success { get; set; }
    }
    class OutLoadKeysEx
    {
        public bool Success { get; set; }
        public string? Details { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class LoadKeys : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                var OutLoadKeys = new OutLoadKeys();
                Task<LoadKeysResponse> response = POSAutoservicio.Instance.LoadKeys();
                response.Wait();

                string temp = JsonSerializer.Serialize(response.Result);
                InLoadKeys? InJson = JsonSerializer.Deserialize<InLoadKeys>(temp);

                OutLoadKeys.Success = true;
                OutLoadKeys.Details = InJson;

                string jsonString = JsonSerializer.Serialize(OutLoadKeys);
                return Ok(OutLoadKeys);
            } catch (Exception ex)
            {
                var OutLoadKeysEx = new OutLoadKeysEx();
                OutLoadKeysEx.Success = false;
                OutLoadKeysEx.Details = ex.Message;
                string jsonString = JsonSerializer.Serialize(OutLoadKeysEx);
                return BadRequest(jsonString);
            }
        }
    }
}
