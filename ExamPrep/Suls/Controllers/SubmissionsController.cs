namespace Suls.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;
    using Suls.Services;
    using Suls.ViewModels.Submissions;

    public class SubmissionsController : Controller
    {
        private readonly ISubmissionsService submissionsService; 
        private readonly IProblemsService problemsService;

        public SubmissionsController(ISubmissionsService submissionsService, IProblemsService problemsService)
        {
            this.submissionsService = submissionsService;
            this.problemsService = problemsService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("Users/Login");
            }

            if (id == null)
            {
                return this.Error("Ivalid Problem!");
            }

            var problemName = this.problemsService.GetNameById(id);
            var viewModel = new SubmissionCreateViewModel
            {
                 Name = problemName,
                 ProblemId = id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(SubmissionCreateInputModel inputModel)
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("Users/Login");
            }

            if (inputModel.Code.Length < 30 || inputModel.Code.Length > 800)
            {
                return this.Error("Code Length should be between 30 and 800 characters");
            }

            this.submissionsService.Crate(this.User, inputModel.ProblemId, inputModel.Code);

            return Redirect("/");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("Users/Login");
            }

            var result = this.submissionsService.Delete(id);

            if (result == 0)
            {
                return this.Error("Submission not removed!");
            }

            return this.Redirect("/");
        }
    }
}
