using ValetCodingExercise.Entities;

namespace ValetCodingExercise.Interfaces
{
    public interface IUserService
    {

        Task<User> Authenticate(string username, string password);
    }
}
