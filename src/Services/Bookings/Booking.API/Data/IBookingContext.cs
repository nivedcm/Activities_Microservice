using Booking.API.Entities;
using MongoDB.Driver;

namespace Booking.API.Data
{
    public interface IBookingContext
    {
        IMongoCollection<Event> Events { get; }
    }
}
