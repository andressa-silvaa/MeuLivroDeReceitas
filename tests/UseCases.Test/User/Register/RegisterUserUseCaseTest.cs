using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.User.Register;
public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Sucess()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUserCase();

        var result = await useCase.Execute(request);

        //SHOULDLY 
        result.ShouldNotBeNull();
        result.Name.ShouldBe(request.Name);
    }

    [Fact]
    public async Task Error_Email_Already_Taken()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUserCase(request.Email);

        Func<Task> act = async () => await useCase.Execute(request);


        //SHOULDLY 
        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        exception.ErrorMessages.Count.ShouldBe(1);
        exception.ErrorMessages.ShouldContain(ResourceMessagesException.EMAIL_ALREADY_REGISTERED);

    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        var useCase = CreateUserCase();

        Func<Task> act = async () => await useCase.Execute(request);


        //SHOULDLY 
        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        exception.ErrorMessages.Count.ShouldBe(1);
        exception.ErrorMessages.ShouldContain(ResourceMessagesException.NAME_EMPTY);

    }

    private RegisterUserUseCase CreateUserCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

        if (!string.IsNullOrEmpty(email)) 
        {
            userReadOnlyRepositoryBuilder.ExistActiveUserWithEmail(email);
        }

        return new RegisterUserUseCase(userReadOnlyRepositoryBuilder.Build(), userWriteOnlyRepository, mapper, passwordEncripter, unitOfWork);
    }
}
