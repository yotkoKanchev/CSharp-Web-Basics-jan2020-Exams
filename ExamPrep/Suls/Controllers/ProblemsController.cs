namespace Suls.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;
    using Suls.Services;
    using Suls.ViewModels.Problems;

    public class ProblemsController : Controller
    {
        private readonly IProblemsService problemsService;

        public ProblemsController(IProblemsService problemsService)
        {
            this.problemsService = problemsService;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateProblemInputModel inputModel)
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("Users/Login");
            }

            if (inputModel.Name.Length < 5 || inputModel.Name.Length > 20)
            {
                return this.Error("Name should be between 5 and 20 charachters!");
            }

            if (inputModel.Points < 50 || inputModel.Points > 300)
            {
                return this.Error("Points should be between 50 and 300 charachters!");
            }

            this.problemsService.Create(inputModel.Name, inputModel.Points);
            return this.Redirect("/");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("Users/Login");
            }

            var viewModel = this.problemsService.GetDetails(id);

            return this.View(viewModel);
        }
    }
}
