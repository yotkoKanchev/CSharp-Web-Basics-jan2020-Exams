namespace IRunes.App.Controllers
{
    using IRunes.App.ViewModels.Tracks;
    using IRunes.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class TracksController : Controller
    {
        private readonly ITracksService tracksService;

        public TracksController(ITracksService tracksService)
        {
            this.tracksService = tracksService;
        }

        public HttpResponse Create(string albumId)
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            var viewModel = new CreateViewModel
            {
                AlbumId = albumId,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(TrackCreateInputModel inputModel)
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            if (inputModel.Name.Length < 4 || inputModel.Name.Length > 20)
            {
                return Error("Track name should be between 4 and 20 characters!");
            }

            if (!inputModel.Link.StartsWith("https://"))
            {
                return Error("Invalid link!");
            }

            if (inputModel.Price < 0)
            {
                return Error("Price can not be negative!");
            }

            this.tracksService.Create(inputModel.Name, inputModel.Link, inputModel.Price, inputModel.AlbumId);

            return Redirect($"/Albums/Details?id={inputModel.AlbumId}");
        }

        public HttpResponse Details(string trackId)
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            var track = this.tracksService.GetDetails(trackId);

            var viewModel = new TrackDetailsViewModel
            {
                AlbumId = track.AlbumId,
                Name = track.Name,
                Link = track.Link,
                Price = track.Price.ToString(),
            };

            return this.View(viewModel);
        }
    }
}
