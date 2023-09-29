using MediatR;
using Microsoft.AspNetCore.Mvc;
using Qi.Vision.WebApi.Features.DocumentAnalysis;
using Qi.Vision.WebApi.Features.DocumentAnalysis.IdentityCard.Front;
using Qi.Vision.WebApi.Filters;

namespace Qi.Vision.WebApi.Controllers
{
    [Route("api/v1/identity-cards")]
    [ApiController]
    [ApiVersion("1.0")]
    public class IdentityCardController : ControllerBase
    {
        private IMediator _mediator;

        public IdentityCardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("front")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public async Task<IActionResult> AnalyzeIdentityCardFrontAsync([FromQuery] TrainningModelType modelType, IFormFile file)
        {
            try
            {
                var result = await _mediator.Send(new AnalyzeIdCardFrontCommand(modelType, file));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("back")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public async Task<IActionResult> AnalyzeIdentityCardBackAsync([FromQuery] TrainningModelType modelType, IFormFile file)
        {
            throw new NotImplementedException("Not implemented yet...");
        }
    }


}
