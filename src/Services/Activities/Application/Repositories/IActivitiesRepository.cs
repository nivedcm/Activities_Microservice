using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IActivitiesRepository
    {
        Task<List<Activity>> GetActivities();
        Task<Activity> GetActivity(Guid id);
        Task<Unit> CreateActivities(Activity activity);
        Task<Unit> EditActivity(Activity activity);
        Task<Unit> DeleteActivity(Guid id);
    }
}
