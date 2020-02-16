namespace Suls.Services
{
    public interface ISubmissionsService
    {
        void Crate(string userId, string problemId, string code);

        int Delete(string id);
    }
}
