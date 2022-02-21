using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;
using Transbank.Responses.AutoservicioResponse;
using System.Text.Json;

namespace api.Controllers
{
    class OutLastSale
    {
        public bool Success { get; set; }
        public InLastSale? Details { get; set; }
    }
    class InLastSale
    {
        public string? Ticket { get; set; }
        public string? AuthorizationCode { get; set; }
        public int Amount { get; set; }
        public int Last4Digits { get; set; }
        public int OperationNumber { get; set; }
        public string? CardType { get; set; }
        public DateTime? AccountingDate { get; set; }
        public string? AccountNumber { get; set; }
        public string? CardBrand { get; set; }
        public DateTime RealDate { get; set; }
        public List<string>? PrintingField { get; set; }
        public int SharesType { get; set; }
        public int SharesNumber { get; set; }
        public int SharesAmount { get; set; }
        public string? SharesTypeGloss { get; set; }
        public long CommerceCode { get; set; }
        public string? TerminalId { get; set; }
        public string? Response { get; set; }
        public int FunctionCode { get; set; }
        public string? ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
        public bool Success { get; set; }
    }
    class OutLastSaleEx
    {
        public bool Success { get; set; }
        public string? Details { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class LastSaleController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                var OutLastSale = new OutLastSale();
                Task<LastSaleResponse> response = POSAutoservicio.Instance.LastSale(true);
                response.Wait();

                string temp = JsonSerializer.Serialize(response.Result);
                InLastSale? InJson = JsonSerializer.Deserialize<InLastSale>(temp);

                OutLastSale.Success = true;
                OutLastSale.Details = InJson;

                string jsonString = JsonSerializer.Serialize(OutLastSale);
                return Ok(jsonString);
            } catch (Exception ex)
            {
                var OutLastSaleEx = new OutLastSaleEx();
                OutLastSaleEx.Success = false;
                OutLastSaleEx.Details = ex.Message;
                string jsonString = JsonSerializer.Serialize(OutLastSaleEx);
                return BadRequest(jsonString);
            }
        }
    }
}
