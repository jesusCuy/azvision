using MediatR;

namespace Qi.Vision.WebApi.Features.DocumentAnalysis.IdentityCard.Back
{
    public class AnalyzeIdCardBackCommand : IRequest<IdentityCardBackAnalysisResult>
    {
        public IFormFile File { get; }
        public TrainningModelType ModelType { get; set; }

        public AnalyzeIdCardBackCommand(TrainningModelType modelType, IFormFile file)
        {
            ModelType = modelType;
            File = file;
        }
    }
}
