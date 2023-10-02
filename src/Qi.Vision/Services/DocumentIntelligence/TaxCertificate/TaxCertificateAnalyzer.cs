using Microsoft.Extensions.Options;
using Qi.Vision.WebApi.Features;
using Qi.Vision.WebApi.Features.TaxCertificate;

namespace Qi.Vision.WebApi.Services.DocumentIntelligence.TaxCertificate
{
    public class TaxCertificateAnalyzer : ITaxCertificateAnalyzer
    {

        private DocAnalyzer _docAnalyzer;
        public DocAnalysisOptions _options { get; set; }

        public TaxCertificateAnalyzer(DocAnalyzer docAnalyzer, IOptions<DocAnalysisOptions> options)
        {
            _docAnalyzer = docAnalyzer;
            _options = options.Value;
        }

        public async Task<TaxCertificateResponse> AnalyzeTaskCertificateAsync(string filePath, TrainningModelType modelType = TrainningModelType.Template)
        {
            var modelId = modelType switch
            {
                TrainningModelType.Template => _options.TAX_CERTIFICATE_TEMPLATE,
                TrainningModelType.Neural => _options.TAX_CERTIFICATE_NEURAL,
                TrainningModelType.Composed => throw new NotImplementedException($"Not implemented yet: {nameof(TrainningModelType.Composed)}"),
                _ => _options.TAX_CERTIFICATE_TEMPLATE
            };

            var analysis = await _docAnalyzer.AnalyzeDocumentAsync(filePath, modelId);

            var result = new TaxCertificateAnalysisResult(analysis.Value);

            return new TaxCertificateResponse
            {
                Name = result.Name,
                FirstLastName = result.FirstLastName,
                SecondLastName = result.SecondLastName,
                RFC = result.RFC,
                CURP = result.CURP,
                ZipCode = result.ZipCode
            };
        }
    }
}
