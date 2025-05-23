using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace WebApi.Controllers
{
    [Route("api/[controller]/webhook")]
    [ApiController]
    public class ClickUpController : ControllerBase
    {
        [HttpGet]
        public IActionResult ValidateWebhook()
        {
            return Ok(198634);
        }

        [HttpPost]
        public IActionResult Create(JsonObject param)
        {
            return Ok(param);
        }
    }
}
