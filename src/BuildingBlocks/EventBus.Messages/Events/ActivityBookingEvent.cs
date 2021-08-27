using System;

namespace EventBus.Messages.Events
{
    public class ActivityBookingEvent : IntegrationBaseEvent
    {
        //Activity details
        public Guid ActivityId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }

        // Bookinng details
        public string BookingId { get; set; }
        public string EventName { get; set; }
        public string Summary { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
        public int Seats { get; set; }
        public Guid UserId { get; set; }
    }
}
