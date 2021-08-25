using Booking.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Booking.API.Data
{
    public class BookingContext : IBookingContext
    {

        public BookingContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Events = database.GetCollection<Event>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            BookingSeedData.SeedData(Events);
        }
        public IMongoCollection<Event> Events { get; }
    }
}
