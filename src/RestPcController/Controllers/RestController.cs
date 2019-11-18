using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace RestPcController.Controllers
{
    [ApiController]
    [Route("api")]
    public class RestController : ControllerBase
    {
        [HttpGet("turnoff")]
        public IActionResult TurnOffPc([FromQuery] string delay = "1")
        {
            var processInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $"/C shutdown -s -f -t {delay}",
                CreateNoWindow = true
            };

            using var process = new Process
            {
                StartInfo = processInfo
            };
            process.Start();
            return Ok();
        }
    }
}
