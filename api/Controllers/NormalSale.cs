using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;
using Transbank.Responses.AutoservicioResponse;
using System.Text.Json;

namespace api.Controllers
{
    class OutNormalSale
    {
        public bool Success { get; set; }
        public InNormalSale? Details { get; set; }
    }
    class InNormalSale
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
    class OutNormalSaleEx
    {
        public bool Success { get; set; }
        public string? Details { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class NormalSaleController : ControllerBase
    {
        [HttpPost(Name = "NormalSale")]
        public IActionResult Post(int monto, bool voucher)
        {
            try
            {
                var OutNormalSale = new OutNormalSale();
                Task<SaleResponse> response = POSAutoservicio.Instance.Sale(monto, "123456789", voucher, true);
                response.Wait();

                string temp = JsonSerializer.Serialize(response.Result);
                InNormalSale? InJson = JsonSerializer.Deserialize<InNormalSale>(temp);

                OutNormalSale.Success = true;
                OutNormalSale.Details = InJson;

                string jsonString = JsonSerializer.Serialize(OutNormalSale);
                return Ok(jsonString);
            } catch (Exception ex)
            {
                var OutNormalSaleEx = new OutNormalSaleEx();
                OutNormalSaleEx.Success = false;
                OutNormalSaleEx.Details = ex.Message;
                string jsonString = JsonSerializer.Serialize(OutNormalSaleEx);
                return BadRequest(jsonString);
            }
        }
    }
}