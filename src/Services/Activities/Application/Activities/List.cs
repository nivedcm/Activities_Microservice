using Application.Repositories;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<Activity>> { }
        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly IActivitiesRepository _activitiesRepository;

            public Handler(IActivitiesRepository activitiesRepository)
            {
                _activitiesRepository = activitiesRepository;
            }

            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _activitiesRepository.GetActivities();
            }
        }
    }
}
