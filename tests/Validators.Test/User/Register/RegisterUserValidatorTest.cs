using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;
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

    [Fact]
    public void Error_Name_Empty()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        var result = validator.Validate(request);

        //SHOULDLY 
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.NAME_EMPTY));
    }

    [Fact]
    public void Error_Email_Empty()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;
        var result = validator.Validate(request);

        //SHOULDLY 
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = "email_invalid";
        var result = validator.Validate(request);

        //SHOULDLY 
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
    }

    [Fact]
    public void Error_Password_Empty()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = string.Empty;
        var result = validator.Validate(request);

        //SHOULDLY 
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.PASSWORD_EMPTY));
    }

    [Fact]
    public void Error_Password_Invalid()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = "jQAAHm82g5eDM7%<(-P63.";
        var result = validator.Validate(request);

        //SHOULDLY 
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.PASSWORD_LIMIT_CHAR));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    public void Error_Password_Valid(int passwordLength)
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

        var result = validator.Validate(request);

        //SHOULDLY 
        result.IsValid.ShouldBeTrue();
    }
}
