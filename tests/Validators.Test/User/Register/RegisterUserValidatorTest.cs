using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using Shouldly;

namespace Validators.Test.User.Register;
public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        var result = validator.Validate(request);

        //SHOULDLY 
        result.IsValid.ShouldBeTrue();

        //Assert.True(result.IsValid) - modo nativo .NET;
        //result.IsValid.Should().BeTrue() - FluenteAssertion(lib paga);
    }
}
