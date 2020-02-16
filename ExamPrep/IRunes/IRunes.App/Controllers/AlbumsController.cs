namespace IRunes.App.Controllers
{
    using IRunes.App.ViewModels.Albums;
    using IRunes.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.Collections.Generic;
    using System.Linq;

    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumsService;

        public AlbumsController(IAlbumsService albumsService)
        {
            this.albumsService = albumsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            IEnumerable<AlbumInfoViewModel> albums = this.albumsService.GetAll().Select(a => new AlbumInfoViewModel
            {
                Name = a.Name,
                Id = a.Id,
            }).ToList();

            var viewModel = new AllAlbumsViewModel
            {
                Albums = albums,
            };

            return this.View(viewModel);
        }

        public HttpResponse Create()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(AlbumCreateInputModel inputModel)
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            if (inputModel.Name.Length < 4 || inputModel.Name.Length > 20)
            {
                return Error("Album Name should be between 4 and 20 characters!");
            }

            if (string.IsNullOrEmpty(inputModel.Cover) || string.IsNullOrWhiteSpace(inputModel.Cover))
            {
                return Error("Cover can not be empty!");
            }

            this.albumsService.Create(inputModel.Name, inputModel.Cover);

            return Redirect("/Albums/All");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            var tracksAndalbum = this.albumsService.GetAlbumAndTracksById(id);
            var album = tracksAndalbum.Item2;
            var tracks = tracksAndalbum.Item1;

            var viewModel = new AlbumDetailsViewModel
            {
                Id = album.Id,
                Name = album.Name,
                Cover = album.Cover,
                Price = album.Price.ToString(),
                Tracks = tracks.Select(t => new TrackInfoViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                }),
            };

            return this.View(viewModel);
        }
    }
}
