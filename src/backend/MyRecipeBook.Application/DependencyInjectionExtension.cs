﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
        AddPasswordEncrypter(services);
    }   
    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped<IMapper>(provider =>
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            return configuration.CreateMapper();
        });
    }
    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
    private static void AddPasswordEncrypter(IServiceCollection services)
    {
        services.AddScoped(option => new PasswordEncripter());
    }
}
