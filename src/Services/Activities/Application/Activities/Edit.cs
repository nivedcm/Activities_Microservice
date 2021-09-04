using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IActivitiesRepository _activitiesRepository;

            public Handler(IActivitiesRepository activitiesRepository)
            {
                _activitiesRepository = activitiesRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _activitiesRepository.EditActivity(request.Activity);
                return activity;
            }
        }
    }
}
