using Application.Repositories;
using Domain;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class Create
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
                var result =  await _activitiesRepository.CreateActivities(request.Activity);
                return result;
            }
        }
    }
}
