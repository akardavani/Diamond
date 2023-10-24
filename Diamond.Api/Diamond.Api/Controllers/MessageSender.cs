using Diamond.API.Controllers;
using Diamond.MessageManager;
using Microsoft.AspNetCore.Mvc;

namespace Diamond.Api.Controllers
{
    public class MessageSender : ApiBaseController
    {
        private readonly IEmailSender _messageSender;

        public MessageSender(IEmailSender messageSender)
        {
            _messageSender = messageSender;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(CancellationToken cancellation)
        {
            var result = await _messageSender.SendAsync("eblaeem@yahoo.com", "Hello world", "testbody");
            return RedirectToAction("Index",cancellation);
        }
    }
}
