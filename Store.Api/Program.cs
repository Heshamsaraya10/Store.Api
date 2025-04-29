 using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Presistence;
using Presistence.Data;
using Presistence.Identity;
using Presistence.Repositories;
using Services;
using Services.Abstractions;
using Services.MappingProfiles;
using Shared.IdentityDtos;
using StackExchange.Redis;
using Store.Api.Extentions;
using Store.Api.Factories;
using Store.Api.MiddleWares;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;



namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddInfraStructureService(builder.Configuration);
            builder.Services.AddCoreServices(builder.Configuration);
            builder.Services.AddPresentationServices();

            var app = builder.Build();

            await app.seedDbAsync();
            app.UseMiddleware<GlobalErrorHandlingMiddleWare>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }


    }
}
