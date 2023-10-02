using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Qi.Vision.WebApi.Filters
{
    public class AllowedExtensionsAttribute : ActionFilterAttribute
    {
        private readonly string[] _allowedExtensions;

        public AllowedExtensionsAttribute(string[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions.Split(",");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var file = context.HttpContext.Request.Form.Files.FirstOrDefault();

            if (file != null)
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_allowedExtensions.Contains(fileExtension))
                {
                    context.Result = new BadRequestObjectResult($"Invalid file extension. Extensions allowed: {_allowedExtensionsString}.");
                }
            }
        }

        private string _allowedExtensionsString => string.Join(", ", _allowedExtensions);
    }
}
