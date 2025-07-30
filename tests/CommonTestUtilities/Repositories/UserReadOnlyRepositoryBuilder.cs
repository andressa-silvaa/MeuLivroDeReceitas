using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories;
public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepository;

    public UserReadOnlyRepositoryBuilder() => _userReadOnlyRepository = new Mock<IUserReadOnlyRepository>();

    public IUserReadOnlyRepository Build() => _userReadOnlyRepository.Object;

    public void ExistActiveUserWithEmail(string email)
    {
        _userReadOnlyRepository.Setup(repository => repository.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
    }
}
