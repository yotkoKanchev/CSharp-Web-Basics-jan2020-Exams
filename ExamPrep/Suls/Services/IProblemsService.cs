namespace Suls.Services
{
    using Suls.ViewModels.Problems;

    public interface IProblemsService
    {
        void Create(string name, int points);

        ProblemDetailsViewModel GetDetails(string problemId);

        string GetNameById(string id);
    }
}
