using FightClubWebApp.Models;

namespace FightClubWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Fight>> GetAllUserRaces();
        Task<List<Club>> GetAllUserCLubs();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
