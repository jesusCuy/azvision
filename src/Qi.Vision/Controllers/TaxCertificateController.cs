using MediatR;
using Microsoft.AspNetCore.Mvc;
using Qi.Vision.WebApi.Filters;
using Qi.Vision.WebApi.Features;
using Qi.Vision.WebApi.Features.TaxCertificate;

namespace Qi.Vision.WebApi.Controllers
{
    [Route("api/v1/tax-certificates")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TaxCertificateController : ControllerBase
    {
        private IMediator _mediator;

        public TaxCertificateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".pdf" })]
        public async Task<IActionResult> AnalyzeTaxCertificateAsync([FromQuery] TrainningModelType modelType, IFormFile file)
        {
            var result = await _mediator.Send(new AnalyzeTaxCertificateCommand(modelType, file));
            return Ok(result);
        }
    }
}
