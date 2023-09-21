﻿using Mc2.CrudTest.framework.Mediator.Abstracts;
using Mc2.CrudTest.Infrastructure.Write.Persistence.DbContexts;
using Mc2.CrudTest.Infrastructure.Write.Persistence.Repository;
using Mc2.CrudTest.Infrastructure.Write.Persistence.Repository.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Infrastructure.Write.Persistence;

public static class DependencyInjection
{
    public static void AddWriteInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationWriteDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("WriteDbConnectionString"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("WriteDbConnectionString"))
            ));

        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ICustomerWriteRepository, CustomerWriteRepository>();
        services.AddTransient(typeof(IGenericWriteRepository<,>), typeof(GenericWriteRepository<,>));
    }
}