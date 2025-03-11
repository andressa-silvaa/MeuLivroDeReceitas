using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.API.UseCases.User.Register;
using MyRecipeBook.Comunication.Requests;
using MyRecipeBook.Comunication.Responses;

namespace MyRecipeBook.API.Controllers;
[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public IActionResult Register(RequestRegisterUserJson request)
    {
        var userCase = new RegisterUserUseCase();
        var result = userCase.Execute(request);
        return Created(string.Empty,result);
    }
}
