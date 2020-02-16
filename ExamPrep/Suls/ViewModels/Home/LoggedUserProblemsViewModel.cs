namespace Suls.ViewModels.Home
{
    using Suls.ViewModels.Users;
    using System.Collections.Generic;

    public class LoggedUserProblemsViewModel
    {
        public IEnumerable<ProblemsUserViewModel> Problems { get; set; }
    }
}
