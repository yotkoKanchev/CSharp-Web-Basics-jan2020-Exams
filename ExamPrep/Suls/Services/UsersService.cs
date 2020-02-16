namespace Suls.Services
{
    using Suls.Data;
    using Suls.Models;
    using Suls.ViewModels.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class UsersService : IUsersService
    {
        private readonly SulsDbContext db;

        public UsersService(SulsDbContext db)
        {
            this.db = db;
        }

        public void Create(string username, string email, string password)
        {
            var hashedPasswod = Hash(password);

            var user = new User
            {
                Username = username,
                Email = email,
                Password = hashedPasswod,
            };

            db.Users.Add(user);
            db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var hashedPassword = this.Hash(password);
            var userId = this.db.Users
                .Where(u => u.Username == username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();
           
            return userId;
        }

        public IEnumerable<ProblemsUserViewModel> ListUserProblems(string userId)
        {
            var problems = this.db.Problems.Select(p => new ProblemsUserViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Count = p.Submissions.Count.ToString(),
            });

            return problems;
        }

        public bool EmailExists(string email)
        {
            return this.db.Users.Any(u => u.Email == email);
        }

        public bool UsernameExists(string username)
        {
            return this.db.Users.Any(u => u.Username == username);
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
