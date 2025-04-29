using Microsoft.AspNetCore.Mvc;
using Store.Api.Factories;
using System.Text.Json.Serialization;

namespace Store.Api.Extentions
{
    public static class PresentationServiceExtention
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(option =>
               option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        } 


    }
}
