namespace Suls.Services
{
    using Suls.Data;
    using Suls.Models;
    using System;
    using System.Linq;

    public class SubmissionsService : ISubmissionsService
    {
        private readonly SulsDbContext db;
        private readonly Random random;

        public SubmissionsService(SulsDbContext db)
        {
            this.db = db;
            this.random = new Random();
        }
        public void Crate(string userId, string problemId, string code)
        {
            var problemMaxPoints = this.db.Problems.First(p => p.Id == problemId).Points;

            var submission = new Submission
            {
                Code = code,
                ProblemId = problemId,
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                AchivedResult = this.random.Next(1, problemMaxPoints + 1),
            };

            this.db.Submissions.Add(submission);
            this.db.SaveChanges();
        }

        public int Delete(string id)
        {
            var submission = this.db.Submissions.Find(id);

            this.db.Submissions.Remove(submission);
            return this.db.SaveChanges();
        }
    }
}
