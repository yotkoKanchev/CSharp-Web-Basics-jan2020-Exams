namespace SharedTrip.Controllers
{
    using IRunes.App.ViewModels.Users;
    using SharedTrip.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.Text.RegularExpressions;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel inputModel)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(inputModel.Username, inputModel.Password);

            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignIn(userId);
            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel inputModel)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            if (inputModel.Password.Length < 6 || inputModel.Password.Length > 20)
            {
                return this.View();
            }

            if (inputModel.Username.Length < 5 || inputModel.Username.Length > 20)
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(inputModel.Email) || string.IsNullOrWhiteSpace(inputModel.Email))
            {
                return this.View();
            }

            if (inputModel.Password != inputModel.ConfirmPassword)
            {
                return this.View();
            }

            if (this.usersService.EmailExists(inputModel.Email))
            {
                return this.View();
            }

            if (this.usersService.UsernameExists(inputModel.Username))
            {
                return this.View();
            }

            this.usersService.Register(inputModel.Username, inputModel.Password, inputModel.Email);

            return Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.SignOut();
            return Redirect("/");
        }
    }
}
