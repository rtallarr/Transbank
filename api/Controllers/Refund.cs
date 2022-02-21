using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;
using System.Text.Json;
using Transbank.Responses.CommonResponses;

namespace api.Controllers
{
    class OutRefund
    {
        public bool Success { get; set; }
        public InRefund? Details { get; set; }
    }
    class InRefund
    {
        public long CommerceCode { get; set; }
        public string? TerminalId { get; set; }
        public string? Response { get; set; }
        public int FunctionCode { get; set; }
        public string? ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
        public bool Success { get; set; }
    }
    class OutRefundEx
    {
        public bool Success { get; set; }
        public string? Details { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class Refund : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                var OutRefund = new OutRefund();
                Task<RefundResponse> response = POSAutoservicio.Instance.Refund();
                response.Wait();

                string temp = JsonSerializer.Serialize(response.Result);
                InRefund? InJson = JsonSerializer.Deserialize<InRefund>(temp);

                OutRefund.Success = true;
                OutRefund.Details = InJson;

                string jsonString = JsonSerializer.Serialize(OutRefund);
                return Ok(jsonString);
            } catch (Exception ex)
            {
                var OutRefundEx = new OutRefundEx();
                OutRefundEx.Success = false;
                OutRefundEx.Details = ex.Message;
                string jsonString = JsonSerializer.Serialize(OutRefundEx);
                return BadRequest(jsonString);
            }
        }
    }
}
