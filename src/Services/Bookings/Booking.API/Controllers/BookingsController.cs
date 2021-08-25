using Booking.API.Entities;
using Booking.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IEventRepository _repository;

        public BookingsController(IEventRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public IEnumerable<Event> GetEvents()
        {
            return (IEnumerable<Event>)_repository.GetEvents();
        }
    }
}
