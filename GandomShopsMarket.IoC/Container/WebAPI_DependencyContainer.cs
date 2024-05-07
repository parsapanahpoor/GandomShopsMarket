#region Usings

using GandomShopsMarket.Infrastructure.Repositories.User;
using Microsoft.Extensions.DependencyInjection;
using GandomShopsMarket.Application.Common.IUnitOfWork;
using GandomShopsMarket.Domain.IRepositories.User;
using GandomShopsMarket.Infrastructure.UnitOfWork;
using GandomShopsMarket.Domain.IRepositories.Role;
using GandomShopsMarket.Infrastructure.Repositories.Role;

namespace GandomShopsMarket.IoC;

#endregion

public static class WebAPI_DependencyContainer
{
    public static void ConfigureDependencies(IServiceCollection services)
    {
        //User
        services.AddScoped<IUserCommandRepository, UserCommandRepository>();
        services.AddScoped<IUserQueryRepository, UserQueryRepository>();

        //Role
        services.AddScoped<IRoleCommandRepository, RoleCommandRepository>();
        services.AddScoped<IRoleQueryRepository, RoleQueryRepository>();

        //Unit Of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
