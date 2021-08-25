using Booking.API.Data;
using Booking.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking.API.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IBookingContext _context;
        public EventRepository(IBookingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateEvent(Event eventModel)
        {
            await _context.Events.InsertOneAsync(eventModel);
        }

        public async Task<bool> DeleteEvent(string id)
        {
            FilterDefinition<Event> filter = Builders<Event>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context.Events.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await _context.Events.Find(x => true).ToListAsync();
        }

        public async Task<Event> GetEvents(string id)
        {
            return await _context.Events.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsByCategory(string categoryName)
        {
            FilterDefinition<Event> filter = Builders<Event>.Filter.Eq(p => p.Category, categoryName);
            return await _context.Events.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateEvent(Event eventModel)
        {
            var updatedresult = await _context.Events.ReplaceOneAsync(filter: x => x.Id == eventModel.Id, replacement: eventModel);

            return updatedresult.IsAcknowledged && updatedresult.ModifiedCount > 0;
        }
    }
}
