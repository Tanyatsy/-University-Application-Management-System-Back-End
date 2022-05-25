using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unipply.Services;
using Unipply.Models;

namespace Unipply.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IEmailSender _emailSender;

        public EmailController(
            ILogger<EmailController> logger,
            IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Route("send")]
        public IActionResult SendEmail([FromBody] EmailModel model)
        {
            _emailSender.Send(model);
            return Ok();
        }
    }
}
