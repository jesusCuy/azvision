using MediatR;
using Microsoft.Extensions.Options;

namespace Qi.Vision.WebApi.Features.DocumentAnalysis.IdentityCard.Back
{
    public class AnalyzeIdCardBackHandler : IRequestHandler<AnalyzeIdCardBackCommand, IdentityCardBackAnalysisResult>
    {
        private const string UPLOADS_FOLDER_NAME = "identity-cards/back";
        private readonly DocSaver _docSaver;
        private readonly DocAnalyzer _docAnalyzer;
        public DocAnalysisOptions _options { get; set; }

        public AnalyzeIdCardBackHandler(
            DocSaver fileSaver, 
            DocAnalyzer docAnalyzer, 
            IOptions<DocAnalysisOptions> options)
        {
            _docSaver = fileSaver;
            _docAnalyzer = docAnalyzer;
            _options = options.Value;
        }

        public async Task<IdentityCardBackAnalysisResult> Handle(AnalyzeIdCardBackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var path = _docSaver.SaveFileAs(request.File, UPLOADS_FOLDER_NAME);

                var modelId = request.ModelType switch
                {
                    TrainningModelType.Template => _options.ID_CARD_BACK_TEMPLATE,
                    TrainningModelType.Neural => throw new NotImplementedException($"Not implemented yet: {nameof(TrainningModelType.Neural)}"),
                    TrainningModelType.Composed => throw new NotImplementedException($"Not implemented yet: {nameof(TrainningModelType.Composed)}"),
                    _ => _options.ID_CARD_BACK_TEMPLATE
                };

                var analysis = await _docAnalyzer.AnalyzeDocumentAsync(path, modelId);

                return new IdentityCardBackAnalysisResult(analysis.Value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"An error occurred during file analysis: {ex.Message}");
            }
        }
    }
}
