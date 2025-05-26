using Application.UseCases.CreateWebhook;
using Application.UseCases.DeleteWebhook;
using Application.UseCases.GetWebhooks;
using Application.UseCases.UpdateWebhook;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClickUpController : ControllerBase
    {
        private readonly GetWebhooksUseCase _getUseCase;
        private readonly CreateWebhookUseCase _createUseCase;
        private readonly UpdateWebhookUseCase _updateUseCase;
        private readonly DeleteWebhookUseCase _deleteUseCase;

        public ClickUpController(GetWebhooksUseCase getUseCase, CreateWebhookUseCase createUseCase, UpdateWebhookUseCase updateUseCase, DeleteWebhookUseCase deleteUseCase)
        {
            _getUseCase = getUseCase;
            _createUseCase = createUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpGet("webhook/{team_id:long}")]
        public async Task<IActionResult> Get(long team_id)
        {
            try
            {
                var result = await _getUseCase.ExecuteAsync(new GetWebhooksInput(team_id));
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
                var result = await _createUseCase.ExecuteAsync(input);
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
                var result = await _updateUseCase.ExecuteAsync(input);
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
                await _deleteUseCase.ExecuteAsync(new DeleteWebhookInput(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
