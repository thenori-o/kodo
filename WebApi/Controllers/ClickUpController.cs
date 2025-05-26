using ClickUpSdk;
using ClickUpSdk.Client;
using ClickUpSdk.DTOs.Webhook;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClickUpController : ControllerBase
    {
        private readonly ClickUpClient _clickUpClient;
        private readonly ClickUpSettings _settings;

        public ClickUpController(ClickUpClient clickUpClient, IOptions<ClickUpSettings> settings)
        {
            _clickUpClient = clickUpClient;
            _settings = settings.Value;
        }

        [HttpGet("webhook/{team_id:long}")]
        public async Task<IActionResult> Get(long team_id)
        {
            try
            {
                var result = await _clickUpClient.GetWebhooksAsync(team_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Create(CreateWebhookRequest createReq)
        {
            try
            {
                var result = await _clickUpClient.CreateWebhookAsync(createReq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("webhook/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateWebhookRequest updateReq)
        {
            try
            {
                var result = await _clickUpClient.UpdateWebhookAsync(id, updateReq);
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
                await _clickUpClient.DeleteWebhookAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
