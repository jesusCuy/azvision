using MediatR;

namespace Qi.Vision.WebApi.Features.DocumentAnalysis.IdentityCard.Front
{
    public class AnalyzeIdCardFrontCommand : IRequest<IdentityCardAnalysisResult>
    {
        public IFormFile File { get; }
        public TrainningModelType ModelType { get; set; }

        public AnalyzeIdCardFrontCommand(TrainningModelType modelType, IFormFile file)
        {
            ModelType = modelType;
            File = file;
        }
    }
}
