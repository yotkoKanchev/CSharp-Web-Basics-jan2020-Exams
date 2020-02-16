namespace IRunes.Services
{
    using IRunes.Data;
    using IRunes.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class AlbumsService : IAlbumsService
    {
        private readonly IRunesDbContext db;

        public AlbumsService(IRunesDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, string cover)
        {
            var album = new Album
            {
                Name = name,
                Cover = cover,
                Price = 0.0M,
            };

            this.db.Albums.Add(album);
            this.db.SaveChanges();
        }

        public (List<Track>, Album) GetAlbumAndTracksById(string id)
        {
            var tracks = db.Tracks.Where(t => t.AlbumId == id).ToList();
            var album = db.Albums.Find(id);

            return (tracks, album);
        }

        public IList<Album> GetAll()
        {
            var albums = this.db.Albums.ToList();
            return albums;
        }
    }
}
