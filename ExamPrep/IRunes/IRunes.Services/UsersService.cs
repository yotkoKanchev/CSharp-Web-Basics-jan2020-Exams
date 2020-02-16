namespace IRunes.Services
{
    using IRunes.Data;
    using IRunes.Models;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class UsersService : IUsersService
    {
        private readonly IRunesDbContext db;

        public UsersService(IRunesDbContext db)
        {
            this.db = db;
        }        

        public string GetUserId(string username, string password)
        {
            var hashedPassword = Hash(password);

            var userId = db.Users
                .Where(u => u.Username == username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            return userId;
        }

        public void Register(string username, string password, string email)
        {
            var user = new User
            {
                 Username = username,
                 Password = this.Hash(password),
                 Email = email,
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public string GetUsername(string id)
        {
            var username = db.Users.Where(u => u.Id == id).Select(u => u.Username).FirstOrDefault();
            return username;
        }

        public bool EmailExists(string email)
        {
            return db.Users.Any(u => u.Email == email);
        }

        public bool UsernameExists(string username)
        {
            return db.Users.Any(u => u.Username == username);
        }

        private string Hash(string input)
        {
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
