using FightClubWebApp.Models;

namespace FightClubWebApp.Interfaces;

public interface IFightRepository
{
    Task<IEnumerable<Fight>> GetAll();
    Task<Fight> GetByIdAsync(int id);
    Task<Fight> GetByIdAsyncNoTracking(int id);
    Task<IEnumerable<Fight>> GetAllFightsByCity(string city);
    bool Add(Fight fight);
    bool Update(Fight fight);
    bool Delete(Fight fight);
    bool Save();
}