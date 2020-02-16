namespace IRunes.Services
{
    using IRunes.Models;
    public interface ITracksService
    {
        void Create(string name, string link, decimal price, string albumId);

        Track GetDetails(string trackId);
    }
}
