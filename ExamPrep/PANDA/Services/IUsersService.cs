﻿namespace PANDA.Services
{
    public interface IUsersService
    {
        void Create(string username, string email, string password);

        string GetUserId(string username, string password);

        bool EmailExists(string email);

        bool UsernameExists(string username);
    }
}
