using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAcess;
using MyRecipeBook.Infrastructure.DataAcess.Repositories;

namespace MyRecipeBook.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        AddDbContext(services);
        AddRepositories(services);
    }
    private static void AddDbContext(IServiceCollection services)
    {
        var connectionString = "Data Source=DESKTOP-PRCKVPI;Initial Catalog=meulivrodereceitas;User ID=andressa_sql_user;Password=@AndressaSilva123;Trusted_Connection=True; Encrypt=True; TrustServerCertificate=True;";
        services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseSqlServer(connectionString);
        });
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork,UnitOfWork>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }
}
