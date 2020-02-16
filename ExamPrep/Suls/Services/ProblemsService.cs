namespace Suls.Services
{
    using Suls.Data;
    using Suls.Models;
    using Suls.ViewModels.Problems;
    using System.Linq;

    public class ProblemsService : IProblemsService
    {
        private readonly SulsDbContext db;

        public ProblemsService(SulsDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, int points)
        {
            var problem = new Problem
            {
                Name = name,
                Points = points,
            };

            this.db.Problems.Add(problem);
            this.db.SaveChanges();
        }

        public ProblemDetailsViewModel GetDetails(string problemId)
        {
            var viewModel = this.db.Problems
                .Where(p => p.Id == problemId)
                .Select(p => new ProblemDetailsViewModel
                {
                    Name = p.Name,
                    Submissions = p.Submissions
                        .Select(s => new SubmissionProblemDetailsViewModel
                        {
                            SubmissionId = s.Id,
                            Username = s.User.Username,
                            AchievedResult = s.AchivedResult.ToString(),
                            MaxPoints = s.Problem.Points.ToString(),
                            CreatedOn = s.CreatedOn.ToString("dd'/'MM'/'yyyy"),
                        }),
                })
                .FirstOrDefault();

            return viewModel;
        }

        public string GetNameById(string id)
        {
            return this.db.Problems.Where(p => p.Id == id).Select(p => p.Name).FirstOrDefault();
        }
    }
}
