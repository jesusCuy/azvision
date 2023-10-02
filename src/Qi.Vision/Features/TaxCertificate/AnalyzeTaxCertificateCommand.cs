using MediatR;
using Qi.Vision.WebApi.Services.DocumentIntelligence.TaxCertificate;

namespace Qi.Vision.WebApi.Features.TaxCertificate
{
    public record AnalyzeTaxCertificateCommand : IRequest<TaxCertificateResponse>
    {
        public IFormFile File { get; }
        public TrainningModelType ModelType { get; set; }

        public AnalyzeTaxCertificateCommand(TrainningModelType modelType, IFormFile file)
        {
            ModelType = modelType;
            File = file;
        }
    }
}
