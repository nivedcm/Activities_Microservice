using Booking.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Data
{
    public class BookingSeedData
    {
        public static void SeedData(IMongoCollection<Event> eventCollection)
        {
            bool validEvent = eventCollection.Find(p => true).Any();
            if(!validEvent)
            {
                eventCollection.InsertManyAsync(GetSeedEvents());
            }
        }

        private static IEnumerable<Event> GetSeedEvents()
        {
            return new List<Event>
            {
                new Event()
                {
                    Id = Guid.NewGuid().ToString(),
                    EventName = "Rahman Show",
                    Category ="Music",
                    Seats = 4500,
                    Price = 5000
                },
                new Event()
                {
                    Id = Guid.NewGuid().ToString(),
                    EventName = "Tomorrowland",
                    Category ="Music",
                    Seats = 12000,
                    Price = 20000
                }
            };
        }
    }
}
