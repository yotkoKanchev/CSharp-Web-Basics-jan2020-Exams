namespace SharedTrip.ViewModels.Trips
{
    using System;

    public class TripInfoViewModel
    {
        public string Id { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public int Seats { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }
    }
}
