using MediatR;
using Qi.Vision.WebApi.Services.Storage;

namespace Qi.Vision.WebApi.Features.IdentityCard.Back
{
    public class AnalyzeIdCardBackHandler : IRequestHandler<AnalyzeIdCardBackCommand, IdentityCardBackResponse>
    {
        private const string UPLOADS_FOLDER_NAME = "identity-cards/back";
        private readonly DocSaver _docSaver;
        private IIdentityCardBackAnalyzer _analyzer { get; set; }

        public AnalyzeIdCardBackHandler(
            DocSaver fileSaver,
            IIdentityCardBackAnalyzer analyzer)
        {
            _docSaver = fileSaver;
            _analyzer = analyzer;
        }

        public async Task<IdentityCardBackResponse> Handle(AnalyzeIdCardBackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var path = _docSaver.SaveFileAs(request.File, UPLOADS_FOLDER_NAME);

                var analysisResult = await _analyzer.AnalyzeIdCardBackAsync(path, request.ModelType);
                return analysisResult;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"An error occurred during file analysis: {ex.Message}");
            }
        }
    }
}
