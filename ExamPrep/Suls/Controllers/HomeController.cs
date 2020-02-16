namespace Suls.App.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;
    using Suls.Services;
    using Suls.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                var problems = this.usersService.ListUserProblems(this.User);

                var viewModel = new LoggedUserProblemsViewModel
                {
                    Problems = problems,
                };

                return this.View(viewModel, "/IndexLoggedIn");
            }

            return this.View("Index");
        }
    }
}