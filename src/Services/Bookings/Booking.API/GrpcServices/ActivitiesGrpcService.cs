using Accounts.Grpc.Protos;
using Application.Repositories;
using System.Threading.Tasks;

namespace Booking.API.GrpcServices
{
    public class ActivitiesGrpcService
    {
        private readonly ActivitiesProtoService.ActivitiesProtoServiceClient _activitiesProtoService;
        public ActivitiesGrpcService(ActivitiesProtoService.ActivitiesProtoServiceClient activitiesProtoService)
        {
            _activitiesProtoService = activitiesProtoService;
        }

        public async Task<ActivitiesModel> GetActivities(string activityId)
        {
            var request = new GetActivitiesRequest { Id = activityId };
            var result = await _activitiesProtoService.GetActivitiesAsync(request);
            return result;
        }
    }
}
