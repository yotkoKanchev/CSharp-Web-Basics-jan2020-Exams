namespace Suls.Services
{
    using Suls.ViewModels.Users;
    using System.Collections.Generic;

    public interface IUsersService
    {
        void Create(string username, string email, string password);

        string GetUserId(string username, string password);

        bool EmailExists(string email);

        bool UsernameExists(string username);

        IEnumerable<ProblemsUserViewModel> ListUserProblems(string userId);
    }
}
