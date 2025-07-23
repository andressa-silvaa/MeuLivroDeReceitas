using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
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

    private RegisterUserUseCase CreateUserCase()
    {
        var mapper = MapperBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder().Build();

        return new RegisterUserUseCase(userReadOnlyRepository, userWriteOnlyRepository, mapper, passwordEncripter, unitOfWork);
    }
}
