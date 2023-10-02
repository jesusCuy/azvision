using MediatR;
using Qi.Vision.WebApi.Services.Storage;

namespace Qi.Vision.WebApi.Features.TaxCertificate
{
    public class AnalyzeTaxCertificateHandler : IRequestHandler<AnalyzeTaxCertificateCommand, TaxCertificateResponse>
    {
        private const string UPLOADS_FOLDER_NAME = "tax-certificates";
        private readonly DocSaver _docSaver;
        private readonly ITaxCertificateAnalyzer _analyzer;

        public AnalyzeTaxCertificateHandler(
            DocSaver fileSaver,
            ITaxCertificateAnalyzer analyzer)
        {
            _docSaver = fileSaver;
            _analyzer = analyzer;
        }


        public async Task<TaxCertificateResponse> Handle(AnalyzeTaxCertificateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var path = _docSaver.SaveFileAs(request.File, UPLOADS_FOLDER_NAME);

                var analysis = await _analyzer.AnalyzeTaskCertificateAsync(path, request.ModelType);
                return analysis;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"An error occurred during file analysis: {ex.Message}");
            }
        }
    }

}
