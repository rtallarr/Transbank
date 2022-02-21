using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;
using System.Text.Json;

namespace api.Controllers
{
    class ConnectionResponse
    {
        public bool Success { get; set; }
        public string? Details { get; set; }
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

        readonly string ports = printCom();

    [HttpPost(Name = "Connect")]
        public IActionResult Post(string portName, int baudrate)
        {
            var connectionResponse = new ConnectionResponse();
            try
            {
                POSAutoservicio.Instance.OpenPort(portName, baudrate);
                Task<bool> pollResult = POSAutoservicio.Instance.Poll();
                pollResult.Wait();

                connectionResponse.Success = pollResult.Result;

                if (pollResult.Result)
                {
                    connectionResponse.Details = $"Succesfully opened port {portName}";
                    string jsonString = JsonSerializer.Serialize(connectionResponse);
                    return Ok(jsonString);
                }
                else
                {
                    connectionResponse.Details = $"Could not opened port {portName}";
                    string jsonString = JsonSerializer.Serialize(connectionResponse);
                    return BadRequest(jsonString);
                }
            } catch (Exception ex)
            {
                connectionResponse.Success = false;
                connectionResponse.Details = ex.Message;
                string jsonString = JsonSerializer.Serialize(connectionResponse);
                return BadRequest(jsonString);
            }
        }
    }
}
