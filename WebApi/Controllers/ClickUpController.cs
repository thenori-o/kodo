using Application.Interfaces;
using Application.UseCases.CreateWebhook;
using Application.UseCases.DeleteWebhook;
using Application.UseCases.GetWebhooks;
using Application.UseCases.UpdateWebhook;
using Infrastructure.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClickUpController : ControllerBase
    {
        private readonly IWebhookService _webhookService;
        private readonly Settings _settings;

        public ClickUpController(IWebhookService webhookService, IOptions<Settings> settings)
        {
            _webhookService = webhookService;
            _settings = settings.Value;
        }

        [HttpGet("webhook/{team_id:long}")]
        public async Task<IActionResult> Get(long team_id)
        {
            try
            {
                var result = await _webhookService.GetAsync(new GetWebhooksInput(team_id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Create(CreateWebhookInput input)
        {
            try
            {
                var result = await _webhookService.CreateAsync(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("webhook")]
        public async Task<IActionResult> Update(UpdateWebhookInput input)
        {
            try
            {
                var result = await _webhookService.UpdateAsync(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("webhook/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _webhookService.DeleteAsync(new DeleteWebhookInput(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
