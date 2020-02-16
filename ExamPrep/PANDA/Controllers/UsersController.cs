namespace PANDA.Controllers
{
    using PANDA.Services;
    using PANDA.ViewModels.Users;
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
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(UserLoginInputModel inputModel)
        {
            var userId = this.usersService.GetUserId(inputModel.Username, inputModel.Password);

            if (userId == null)
            {
                return this.Error("Invalid Credentials!");
            }

            this.SignIn(userId);

            return Redirect("/");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterInputModel inputModel)
        {
            var emailPattern = @"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}";
            var emailMatch = Regex.Match(inputModel.Email, emailPattern, RegexOptions.IgnoreCase);

            if (!emailMatch.Success)
            {
                return Error("Email is not valid!");
            }

            if (this.usersService.EmailExists(inputModel.Password))
            {
                return this.Error("Email already used!");
            }

            if (this.usersService.UsernameExists(inputModel.Username))
            {
                return this.Error("Username already used!");
            }

            if (string.IsNullOrEmpty(inputModel.Username) || string.IsNullOrWhiteSpace(inputModel.Username))
            {
                return this.Error("Username can not be empty!");
            }

            if (string.IsNullOrEmpty(inputModel.Password) || string.IsNullOrWhiteSpace(inputModel.Password))
            {
                return this.Error("Password can not be empty!");
            }

            if (string.IsNullOrEmpty(inputModel.ConfirmPassword) || string.IsNullOrWhiteSpace(inputModel.ConfirmPassword))
            {
                return this.Error("ConfirmPassword can not be empty!");
            }

            this.usersService.Create(inputModel.Username, inputModel.Email, inputModel.Password);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
