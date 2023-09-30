﻿using MediatR;
using Microsoft.Extensions.Options;

namespace Qi.Vision.WebApi.Features.DocumentAnalysis.IdentityCard.Front
{
    public class AnalyzeIdCardFrontHandler : IRequestHandler<AnalyzeIdCardFrontCommand, IdentityCardFrontAnalysisResult>
    {
        private const string UPLOADS_FOLDER_NAME = "identity-cards/front";
        private readonly DocSaver _docSaver;
        private readonly DocAnalyzer _docAnalyzer;
        public DocAnalysisOptions _options { get; set; }


        public AnalyzeIdCardFrontHandler(
            DocSaver fileSaver, 
            DocAnalyzer docAnalyzer, 
            IOptions<DocAnalysisOptions> options)
        {
            _docSaver = fileSaver;
            _docAnalyzer = docAnalyzer;
            _options = options.Value;
        }

        public async Task<IdentityCardFrontAnalysisResult> Handle(AnalyzeIdCardFrontCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var path = _docSaver.SaveFileAs(request.File, UPLOADS_FOLDER_NAME);

                var modelId = request.ModelType switch
                {
                    TrainningModelType.Template => _options.ID_CARD_FRONT_TEMPLATE,
                    TrainningModelType.Neural => _options.ID_CARD_FRONT_NEURAL,
                    TrainningModelType.Composed => throw new NotImplementedException($"Not implemented yet: {nameof(TrainningModelType.Composed)}"),
                    _ => _options.ID_CARD_FRONT_TEMPLATE
                };

                var analysis = await _docAnalyzer.AnalyzeDocumentAsync(path, modelId);

                return new IdentityCardFrontAnalysisResult(analysis.Value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"An error occurred during file analysis: {ex.Message}");
            }
        }

    }
}
