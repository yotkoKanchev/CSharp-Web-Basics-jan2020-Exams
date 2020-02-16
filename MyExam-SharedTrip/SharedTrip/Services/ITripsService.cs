namespace SharedTrip.Services
{
    using SharedTrip.ViewModels.Trips;
    using System.Collections.Generic;

    public interface ITripsService
    {
        IEnumerable<TripInfoViewModel> GetAllTrips();

        string AddTrip(AddTripInputModel inputModel);

        TripDetailsViewModel GetTripById(string tripId);

        int AddUserToTrip(string userId, string tripId);
    }
}
