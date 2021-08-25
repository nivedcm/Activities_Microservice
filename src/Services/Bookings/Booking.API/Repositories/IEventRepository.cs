using Booking.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking.API.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEvents();
        Task<Event> GetEvents(string id);
        Task<IEnumerable<Event>> GetEventsByCategory(string categoryName);

        Task<bool> UpdateEvent(Event eventModel);
        Task<bool> DeleteEvent(string id);
        Task CreateEvent(Event eventModel);
    }
}