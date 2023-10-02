using Microsoft.Extensions.Options;
using Qi.Vision.WebApi.Features;
using Qi.Vision.WebApi.Features.IdentityCard.Back;

namespace Qi.Vision.WebApi.Services.DocumentIntelligence.IdentityCard.Back
{
    public class IdentityCardBackAnalyzer : IIdentityCardBackAnalyzer
    {
        private DocAnalyzer _docAnalyzer;
        public DocAnalysisOptions _options { get; set; }

        public IdentityCardBackAnalyzer(DocAnalyzer docAnalyzer, IOptions<DocAnalysisOptions> options)
        {
            _docAnalyzer = docAnalyzer;
            _options = options.Value;
        }

        public async Task<IdentityCardBackResponse> AnalyzeIdCardBackAsync(string filePath, TrainningModelType trainningModelType = TrainningModelType.Template)
        {
            var modelId = trainningModelType switch
            {
                TrainningModelType.Template => _options.ID_CARD_BACK_TEMPLATE,
                TrainningModelType.Neural => throw new NotImplementedException($"Not implemented yet: {nameof(TrainningModelType.Neural)}"),
                TrainningModelType.Composed => throw new NotImplementedException($"Not implemented yet: {nameof(TrainningModelType.Composed)}"),
                _ => _options.ID_CARD_BACK_TEMPLATE
            };

            var analysis = await _docAnalyzer.AnalyzeDocumentAsync(filePath, modelId);

            var result = new IdentityCardBackAnalysisResultMapper(analysis.Value);

            return new IdentityCardBackResponse()
            {
                ElectorId = new FieldAnalysisResult 
                {
                    Value = result?.ElectorId?.Value,
                    Confidence = result?.ElectorId?.Confidence
                }
            };
        }
    }
}
