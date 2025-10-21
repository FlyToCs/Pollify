using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pollify.Application;
using Pollify.Application.Contracts;
using Pollify.Domain.Entities.SurveyAgg;
using Pollify.Domain.Entities.UserAgg;
using Pollify.Infrastructure.EfCore.persistence;
using Pollify.Infrastructure.EfCore.Repositories;

namespace ServiceRegistry
{
    public static class Bootstrapper
    {
        public static IServiceCollection Initialize(this IServiceCollection serviceCollection )
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<ISurveyRepository, SurveyRepository>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<ISurveyService, SurveyService>();
            serviceCollection.AddDbContext<AppDbContext>(x=>
                x.UseSqlServer("Data Source=.;Initial Catalog=SurveyDb;User ID=sa; Password=123456;Trust Server Certificate=True"));

            return serviceCollection;
        }
    }
}
