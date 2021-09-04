using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ActivitiesRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> CreateActivities(Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Unit> DeleteActivity(Guid id)
        {
            var activity = await _context.Activities.FindAsync(id);
            _context.Remove(activity);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Unit> EditActivity(Activity activity)
        {
            var result = await _context.Activities.FindAsync(activity.Id);
            _mapper.Map(result, activity);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<List<Activity>> GetActivities()
        {
            return await _context.Activities.ToListAsync();
        }

        public async Task<Activity> GetActivity(Guid id)
        {
            return await _context.Activities.FindAsync(id);
        }
    }
}
