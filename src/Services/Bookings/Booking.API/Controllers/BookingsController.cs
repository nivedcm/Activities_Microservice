using Booking.API.Entities;
using Booking.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IEventRepository _repository;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(IEventRepository repository, ILogger<BookingsController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Event>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Event>>> GetProducts()
        {
            var products = await _repository.GetEvents();
            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetEvent")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Event), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Event>> GetProductById(string id)
        {
            var eventResult = await _repository.GetEvents(id);
            if (eventResult == null)
            {
                return NotFound();
            }
            return Ok(eventResult);
        }

        [Route("[action]/{category}", Name = "GetEventByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Event>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByCategory(string category)
        {
            var products = await _repository.GetEventsByCategory(category);
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Event), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Event>> CreateEvent([FromBody] Event eventModel)
        {
            await _repository.CreateEvent(eventModel);
            return CreatedAtRoute("GetEvent", new { id = eventModel.Id }, eventModel);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Event), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateEvent([FromBody] Event eventModel)
        {
            return Ok(await _repository.UpdateEvent(eventModel));
        }

        [HttpDelete("{id}", Name = "DeleteEvent")]
        [ProducesResponseType(typeof(Event), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteEventById(string id)
        {
            return Ok(await _repository.DeleteEvent(id));
        }
    }
}
