using MediatR;

namespace Qi.Vision.WebApi.Features.IdentityCard.Front
{
    public record AnalyzeIdCardFrontCommand : IRequest<IdentityCardFrontResponse>
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
