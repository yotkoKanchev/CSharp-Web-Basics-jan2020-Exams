namespace IRunes.Services
{
    using IRunes.Data;
    using IRunes.Models;
    using System.Linq;

    public class TracksService : ITracksService
    {
        private readonly IRunesDbContext db;

        public TracksService(IRunesDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, string link, decimal price, string albumId)
        {
            var track = new Track
            {
                Name = name,
                Link = link,
                Price = price,
                AlbumId = albumId,
            };

            this.db.Tracks.Add(track);

            var totalSum =  this.db.Tracks.Where(t => t.AlbumId == albumId)
                .Sum(t => t.Price) + price;

            var album = this.db.Albums.Find(albumId);
            album.Price = totalSum * 0.87M;

            db.SaveChanges();
        }

        public Track GetDetails(string trackId)
        {
            var track = db.Tracks.FirstOrDefault(x => x.Id == trackId);

                return track;
        }
    }
}
