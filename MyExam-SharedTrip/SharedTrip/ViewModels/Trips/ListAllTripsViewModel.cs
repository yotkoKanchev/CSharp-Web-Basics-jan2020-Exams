namespace SharedTrip.ViewModels.Trips
{
    using System.Collections.Generic;

    public class ListAllTripsViewModel
    {
        public IEnumerable<TripInfoViewModel> Trips { get; set; }
    }
}
