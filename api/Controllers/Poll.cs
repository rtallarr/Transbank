﻿using Microsoft.AspNetCore.Mvc;
using Transbank.POSAutoservicio;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Poll : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
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
