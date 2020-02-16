﻿namespace Suls.App.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;
    using Suls.Services;
    using Suls.ViewModels.Users;
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
        public HttpResponse Login(LoginUserInputModel inputModel)
        {
            var userId = this.usersService.GetUserId(inputModel.Username, inputModel.Password);

            if (userId == null)
            {
                return this.Error("Ivalid credentials!");
            }

            this.SignIn(userId);

            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(CreateUserInputModel inputModel)
        {
            var emailPattern = @"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}";
            var match = Regex.Match(inputModel.Email, emailPattern, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                return Error("Email is not valid!");
            }

            if (this.usersService.EmailExists(inputModel.Email))
            {
                return Error("Email already used!");
            }

            if (inputModel.Username.Length < 5 || inputModel.Username.Length > 20)
            {
                return Error("Username should be between 5 and 20 characters!");
            }

            if (this.usersService.UsernameExists(inputModel.Username))
            {
                return Error("Username already used!");
            }

            if (inputModel.Password.Length < 6 || inputModel.Password.Length > 20)
            {
                return Error("Password should be between 6 and 20 characters!");
            }

            if (inputModel.Password != inputModel.ConfirmPassword)
            {
                return Error("Passwords does not match!");
            }

            this.usersService.Create(inputModel.Username, inputModel.Email, inputModel.Password);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("Users/Login");
            }

            this.SignOut();

            return this.Redirect("/");
        }
    }
}