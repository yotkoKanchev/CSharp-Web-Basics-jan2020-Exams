namespace PANDA.Services
{
    using PANDA.Data;
    using PANDA.Models;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class UsersService : IUsersService
    {
        private readonly PandaDbContext db;

        public UsersService(PandaDbContext db)
        {
            this.db = db;
        }

        public void Create(string username, string email, string password)
        {
            var hashedPassword = Hash(password);
            var user = new User
            {
                Username = username,
                Email = email,
                Password = hashedPassword,
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var hashedPasswod = Hash(password);
            var userId = this.db.Users
                .Where(u => u.Username == username && u.Password == hashedPasswod)
                .Select(u => u.Id)
                .FirstOrDefault();

            return userId;
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
