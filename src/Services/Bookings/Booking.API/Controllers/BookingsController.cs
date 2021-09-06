using Booking.API.Entities;
using Booking.API.GrpcServices;
using Booking.API.Repositories;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ActivitiesGrpcService _activitiesGrpcService;

        public BookingsController(IEventRepository repository, ActivitiesGrpcService activitiesGrpcService)
        {
            _activitiesGrpcService = activitiesGrpcService;
            _repository = repository ?? throw new ArgumentNullException();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Event>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var events = await _repository.GetEvents();
            return Ok(events);
        }

        [HttpGet("{id}", Name = "GetEvent")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Event), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Event>> GetEventById(string id)
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
            var events = await _repository.GetEventsByCategory(category);
            return Ok(events);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Event), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Event>> CreateEvent([FromBody] Event eventModel)
        {
            //Grpc wired up, Testing out Grpc call here

            var activity = await _activitiesGrpcService.GetActivities(eventModel.Id.ToString());

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
