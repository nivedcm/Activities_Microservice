using Application.Repositories;
using AutoMapper;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                var activity = await _activitiesRepository.DeleteActivity(request.Id);
                return activity;
            }
        }
    }
}
