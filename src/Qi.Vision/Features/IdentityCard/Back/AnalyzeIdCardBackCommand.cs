using MediatR;

namespace Qi.Vision.WebApi.Features.IdentityCard.Back
{
    public record AnalyzeIdCardBackCommand : IRequest<IdentityCardBackResponse>
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
