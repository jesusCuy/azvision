using Microsoft.Extensions.Options;
using Qi.Vision.WebApi.Features;
using Qi.Vision.WebApi.Features.IdentityCard.Front;

namespace Qi.Vision.WebApi.Services.DocumentIntelligence.IdentityCard.Front
{
    public class IdentityCardFrontAnalyzer : IIdentityCardFrontAnalyzer
    {
        private DocAnalyzer _docAnalyzer;
        public DocAnalysisOptions _options { get; set; }

        public IdentityCardFrontAnalyzer(DocAnalyzer docAnalyzer, IOptions<DocAnalysisOptions> options)
        {
            _docAnalyzer = docAnalyzer;
            _options = options.Value;
        }

        public async Task<IdentityCardFrontResponse> AnalyzeIdCardFrontAsync(string filePath, TrainningModelType trainningModelType = TrainningModelType.Template)
        {
            var modelId = trainningModelType switch
            {
                TrainningModelType.Template => _options.ID_CARD_FRONT_TEMPLATE,
                TrainningModelType.Neural => _options.ID_CARD_FRONT_NEURAL,
                TrainningModelType.Composed => throw new NotImplementedException($"Not implemented yet: {nameof(TrainningModelType.Composed)}"),
                _ => _options.ID_CARD_BACK_TEMPLATE
            };

            var analysis = await _docAnalyzer.AnalyzeDocumentAsync(filePath, modelId);

            var result = new IdentityCardFrontAnalysisResult(analysis.Value);

            return new IdentityCardFrontResponse()
            {
                FullName = result.Name,
                Address = result.Address,  
                BirthDate = result.BirthDate,
                CURP = result.Curp,
                ElectorKey = result.ElectorId
            };
        }
    }
}
