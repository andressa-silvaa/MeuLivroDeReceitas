﻿using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Comunication.Requests;
using MyRecipeBook.Comunication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.API.UseCases.User.Register;

public class RegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        var passwordEncripter = new PasswordEncripter();

        var autoMapper = new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();

        Validate(request);

        var user = autoMapper.Map<Domain.Entities.User>(request);
        user.Password = passwordEncripter.Encrypter(request.Password);

        await _userWriteOnlyRepository.AddAsync(user);
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
