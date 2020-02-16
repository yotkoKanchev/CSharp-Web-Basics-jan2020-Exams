﻿namespace IRunes.App.Controllers
{
    using IRunes.App.ViewModels.Users;
    using IRunes.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;

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
        public HttpResponse Login(LoginInputModel inputModel)
        {
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
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel inputModel)
        {      
            if (inputModel.Password.Length < 6 || inputModel.Password.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters!");
            }

            if (inputModel.Username.Length < 4 || inputModel.Username.Length > 10)
            {
                return this.Error("Username should be between 4 and 10 characters!");
            }

            if (string.IsNullOrEmpty(inputModel.Email) || string.IsNullOrWhiteSpace(inputModel.Email))
            {
                return this.Error("Email can not be empty!");
            }

            if (inputModel.Password != inputModel.ConfirmPassword)
            {
                return this.Error("Passwords should match!");
            }

            if (this.usersService.EmailExists(inputModel.Email))
            {
                return this.Error("Email already used!");
            }

            if (this.usersService.UsernameExists(inputModel.Username))
            {
                return this.Error("Username already exists!");
            }

            this.usersService.Register(inputModel.Username, inputModel.Password, inputModel.Email);

            return Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return Redirect("/");
        }
    }
}
