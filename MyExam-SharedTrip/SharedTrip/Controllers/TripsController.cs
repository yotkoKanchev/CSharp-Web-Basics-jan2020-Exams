namespace SharedTrip.Controllers
{
    using SharedTrip.Services;
    using SharedTrip.ViewModels.Trips;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var trips = this.tripsService.GetAllTrips();

            var viewModel = new ListAllTripsViewModel
            {
                Trips = trips,
            };

            return this.View(viewModel);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddTripInputModel inputModel)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(inputModel.StartPoint) || string.IsNullOrWhiteSpace(inputModel.StartPoint))
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(inputModel.EndPoint) || string.IsNullOrWhiteSpace(inputModel.EndPoint))
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(inputModel.DepartureTime) || string.IsNullOrWhiteSpace(inputModel.DepartureTime))
            {
                return this.View();
            }

            if (inputModel.Seats < 2 || inputModel.Seats > 6)
            {
                return this.View();
            }

            if (inputModel.Description.Length > 80)
            {
                return this.View();
            }            

            var tripId = this.tripsService.AddTrip(inputModel);

            if (tripId == null)
            {
                return this.View();
            }

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.tripsService.GetTripById(tripId);

            if (viewModel == null)
            {
                return this.View();
            }

            return this.View(viewModel);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var result = this.tripsService.AddUserToTrip(this.User, tripId);

            if (result == 0)
            {
                return this.Details(tripId);
            }
            else
            {
                return this.Redirect("/Trips/All");
            }
        }
    }
}
