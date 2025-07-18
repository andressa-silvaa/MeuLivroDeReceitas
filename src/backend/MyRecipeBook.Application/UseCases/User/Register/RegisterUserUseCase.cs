using AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Comunication.Requests;
using MyRecipeBook.Comunication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private IMapper _mapper;
    private readonly PasswordEncripter _passwordEncripter;

    public RegisterUserUseCase(IUserReadOnlyRepository userReadOnlyRepository, 
        IUserWriteOnlyRepository userWriteOnlyRepository, 
        IMapper mapper, 
        PasswordEncripter passwordEncripter)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypter(request.Password);

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
