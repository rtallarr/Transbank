using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;
using Transbank.Responses.AutoservicioResponse;
using System.Text.Json;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NormalSaleController : ControllerBase
    {
        [HttpPost(Name = "NormalSale")]
        public IActionResult Post(int monto, bool voucher)
        {
            try
            {
                Task<SaleResponse> response = POSAutoservicio.Instance.Sale(monto, "12345", voucher, true);
                response.Wait();
                string jsonString = JsonSerializer.Serialize(response.Result);
                return Ok(jsonString);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}