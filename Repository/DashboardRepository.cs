using Microsoft.EntityFrameworkCore;
using FightClubWebApp.Data;
using FightClubWebApp.Interfaces;
using FightClubWebApp.Models;

namespace FightClubWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Club>> GetAllUserCLubs()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = _dbContext.Clubs.Where(r => r.AppUser.Id == currentUser);
            return userClubs.ToList();
        }

        public async Task<List<Fight>> GetAllUserRaces()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userRaces = _dbContext.Fights.Where(r => r.AppUser.Id == currentUser);
            return userRaces.ToList();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _dbContext.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(AppUser user)
        {
            _dbContext.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved =  _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
