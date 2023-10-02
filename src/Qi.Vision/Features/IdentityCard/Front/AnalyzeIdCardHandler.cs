using MediatR;
using Microsoft.Extensions.Options;
using Qi.Vision.WebApi.Services.DocumentIntelligence;
using Qi.Vision.WebApi.Services.Storage;

namespace Qi.Vision.WebApi.Features.IdentityCard.Front
{
    public class AnalyzeIdCardFrontHandler : IRequestHandler<AnalyzeIdCardFrontCommand, IdentityCardFrontResponse>
    {
        private const string UPLOADS_FOLDER_NAME = "identity-cards/front";
        private readonly DocSaver _docSaver;


        private IIdentityCardFrontAnalyzer _analyzer;
        public DocAnalysisOptions _options { get; set; }


        public AnalyzeIdCardFrontHandler(
            DocSaver fileSaver,
            IIdentityCardFrontAnalyzer analyzer,
            IOptions<DocAnalysisOptions> options)
        {
            _docSaver = fileSaver;
            _analyzer = analyzer;
            _options = options.Value;
        }

        public async Task<IdentityCardFrontResponse> Handle(AnalyzeIdCardFrontCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var path = _docSaver.SaveFileAs(request.File, UPLOADS_FOLDER_NAME);

                var analysisResult = await _analyzer.AnalyzeIdCardFrontAsync(path, request.ModelType);
                return analysisResult;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"An error occurred during file analysis: {ex.Message}");
            }
        }

    }
}
