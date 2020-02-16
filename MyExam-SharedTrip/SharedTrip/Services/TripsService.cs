namespace SharedTrip.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SharedTrip.Models;
    using SharedTrip.ViewModels.Trips;

    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string AddTrip(AddTripInputModel inputModel)
        {
            var dateAsDateTime = DateTime.ParseExact(inputModel.DepartureTime, "dd.MM.yyyy HH:mm",
                                       System.Globalization.CultureInfo.InvariantCulture);

            if (dateAsDateTime == null)
            {
                return null;
            }

            var trip = new Trip
            {
                StartPoint = inputModel.StartPoint,
                EndPoint = inputModel.EndPoint,
                Seats = inputModel.Seats,
                DepartureTime = dateAsDateTime,
                Description = inputModel.Description,
                ImagePath = inputModel.ImagePath,
            };

            db.Trips.Add(trip);
            db.SaveChanges();

            return trip.Id;
        }

        public int AddUserToTrip(string userId, string tripId) //returns 0 if user is alrady joined, and number of affected db changes if not
        {
            if (db.Trips
                .Where(t => t.Id == tripId)
                .Any(t => t.UserTrips
                    .Any(ut => ut.UserId == userId)))
            {
                return  0;
            }
            else
            {
                var currentTrip = db.Trips.Find(tripId);

                if (currentTrip.Seats == 0)
                {
                    return 0;
                }

                currentTrip.Seats -= 1;

                var userTrip = new UserTrip
                {
                    UserId = userId,
                    TripId = tripId,
                };

                db.UsersTrips.Add(userTrip);
            }
            
            return db.SaveChanges();
        }

        public IEnumerable<TripInfoViewModel> GetAllTrips()
        {
            var items = this.db.Trips
                .Select(t => new TripInfoViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime,
                    Seats = t.Seats,
                    Description = t.Description,
                    ImagePath = t.ImagePath,
                })
                .ToList();

            return items;
        }

        public TripDetailsViewModel GetTripById(string tripId)
        {
            var tripInputModel = db.Trips
                .Where(t => t.Id == tripId)
                .Select(t => new TripDetailsViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("MM/dd/yyyy H:mm"),
                    ImagePath = t.ImagePath,
                    Description = t.Description,
                    Seats = t.Seats
                })
                .FirstOrDefault();

            return tripInputModel;
        }
    }
}
