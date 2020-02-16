namespace IRunes.Services
{
    using IRunes.Models;
    using System.Collections.Generic;

    public interface IAlbumsService
    {
        void Create(string name, string cover);

        IList<Album> GetAll();

        (List<Track>, Album) GetAlbumAndTracksById(string id);
    }
}
