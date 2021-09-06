using Accounts.Grpc.Protos;
using Application.Repositories;
using AutoMapper;
using Domain;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Activities.Grpc.Services
{
    public class ActivitiesService : ActivitiesProtoService.ActivitiesProtoServiceBase
    {
        private readonly IActivitiesRepository _activitiesRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ActivitiesService(IActivitiesRepository activitiesRepository, ILogger logger, IMapper mapper)
        {
            _activitiesRepository = activitiesRepository;
            _logger = logger;
            _mapper = mapper;
        }


        public override async Task<ActivitiesModel> GetActivities(GetActivitiesRequest request, ServerCallContext context)
        {
            var activity = await _activitiesRepository.GetActivity(Guid.Parse(request.Id));
            if (activity == null) throw new RpcException(new Status(StatusCode.NotFound, "failed"));

            var activitiesModel = _mapper.Map<ActivitiesModel>(activity);
            return activitiesModel;
        }

        public override async Task<ActivitiesModel> CreateActivities(CreateActivitiesRequest request, ServerCallContext context)
        {
            var activityModel = _mapper.Map<Activity>(request.Activity);

            await _activitiesRepository.CreateActivities(activityModel);

            var activitiesModel = _mapper.Map<ActivitiesModel>(activityModel);
            return activitiesModel;
        }

        public override async Task<ActivitiesModel> UpdateActivities(UpdateActivitiesRequest request, ServerCallContext context)
        {
            var activityModel = _mapper.Map<Activity>(request.Activity);

            await _activitiesRepository.EditActivity(activityModel);

            var activitiesModel = _mapper.Map<ActivitiesModel>(activityModel);
            return activitiesModel;
        }

        public override async Task<DeleteActivitiesResponse> DeleteActivities(DeleteActivitiesRequest request, ServerCallContext context)
        {
            await _activitiesRepository.DeleteActivity(Guid.Parse(request.Id));

            // change this
            var response = new DeleteActivitiesResponse
            {
                Success = true
            };

            return response;
        }
    }
}
