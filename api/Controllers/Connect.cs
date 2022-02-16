using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Connect : ControllerBase
    {
        public static string printCom()
        {
            List<string> ports = POSAutoservicio.Instance.ListPorts();
            int i = 0;
            foreach (string port in ports)
            {
                Console.WriteLine(i + ": " + port);
                i++;
            }
            return ports.ToString();
        }

        int baudrate = 19200;
        string portName = "COM7";
        string puertos = printCom();

    [HttpPost(Name = "Connect")]
        public IActionResult Post()
        {
            try
            {
                POSAutoservicio.Instance.OpenPort(portName, baudrate);
                Task<bool> pollResult = POSAutoservicio.Instance.Poll();
                pollResult.Wait();
                if (pollResult.Result)
                    return Ok("Pos Connected");
                else
                    return Ok("Pos NOT Connected");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
