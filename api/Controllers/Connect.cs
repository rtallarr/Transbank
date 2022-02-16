using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;
using System.Text.Json;

namespace api.Controllers
{
    public class connectionResponse
    {
        public bool success { get; set; }
        public string details { get; set; }
    }

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

        string ports = printCom();

    [HttpPost(Name = "Connect")]
        public IActionResult Post(string portName, int baudrate)
        {
            var connectionResponse = new connectionResponse();
            try
            {
                POSAutoservicio.Instance.OpenPort(portName, baudrate);
                Task<bool> pollResult = POSAutoservicio.Instance.Poll();
                pollResult.Wait();

                connectionResponse.success = pollResult.Result;

                if (pollResult.Result)
                {
                    connectionResponse.details = $"Succesfully opened port {portName}";
                    string jsonString = JsonSerializer.Serialize(connectionResponse);
                    return Ok(jsonString);
                }
                else
                {
                    connectionResponse.details = $"Could not opened port {portName}";
                    string jsonString = JsonSerializer.Serialize(connectionResponse);
                    return BadRequest(jsonString);
                }
            } catch (Exception ex)
            {
                connectionResponse.success = false;
                connectionResponse.details = ex.Message;
                string jsonString = JsonSerializer.Serialize(connectionResponse);
                return BadRequest(jsonString);
            }
        }
    }
}
