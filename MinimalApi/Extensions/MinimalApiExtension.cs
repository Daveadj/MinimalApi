using Application.Abtrastions;
using Application.Posts.Commands;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Abstractions;
using System.Reflection;

namespace MinimalApi.Extensions
{
    public static class MinimalApiExtension
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SocialDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddMediatR(mediate => mediate.RegisterServicesFromAssembly(typeof(CreatePost).GetTypeInfo().Assembly));
        }

        public static void RegisterEndPointDefinitions(this WebApplication app)
        {
            var endPointDefinitions = typeof(Program).Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEndPointDefinition)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndPointDefinition>();

            foreach (var endPointDefinition in endPointDefinitions)
            {
                endPointDefinition.RegisterEndPoints(app);
            }
        }
    }
}