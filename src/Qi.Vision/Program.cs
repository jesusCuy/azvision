using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Qi.Vision.WebApi.Features.IdentityCard.Back;
using Qi.Vision.WebApi.Features.IdentityCard.Front;
using Qi.Vision.WebApi.Features.TaxCertificate;
using Qi.Vision.WebApi.Services.DocumentIntelligence;
using Qi.Vision.WebApi.Services.DocumentIntelligence.IdentityCard.Back;
using Qi.Vision.WebApi.Services.DocumentIntelligence.IdentityCard.Front;
using Qi.Vision.WebApi.Services.DocumentIntelligence.TaxCertificate;
using Qi.Vision.WebApi.Services.Storage;
using System.Reflection;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.Services.Configure<DocAnalysisOptions>(
        builder.Configuration.GetSection(DocAnalysisOptions.DocAnalysis));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c => 
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "API v1",
                Description = "API v1",
            });
        });

        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 80 * 1024 * 1024; // Max file size 80MB due AZ limits
        });

        builder.Services.AddScoped<DocSaver>();
        builder.Services.AddScoped<DocAnalyzer>();
        builder.Services.AddScoped<IIdentityCardFrontAnalyzer, IdentityCardFrontAnalyzer>();
        builder.Services.AddScoped<IIdentityCardBackAnalyzer, IdentityCardBackAnalyzer>();
        builder.Services.AddScoped<ITaxCertificateAnalyzer, TaxCertificateAnalyzer>();

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}