using FightClubWebApp.Data;
using FightClubWebApp.Interfaces;
using FightClubWebApp.Models;
using Microsoft.EntityFrameworkCore;


namespace FightClubWebApp.Repository
{
    public class FightRepository : IFightRepository
    {
        private readonly ApplicationDbContext _context;

        public FightRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Fight fight)
        {
            _context.Add(fight);
            return Save();
        }

        public bool Delete(Fight fight)
        {
            _context.Remove(fight);
            return Save();
        }

        public async Task<IEnumerable<Fight>> GetAll()
        {
            return await _context.Fights.ToListAsync();
        }

        public async Task<Fight> GetByIdAsync(int id)
        {
            return await _context.Fights.Include(a => a.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Fight> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Fights.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Fight>> GetAllFightsByCity(string city)
        {
            return await _context.Fights.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Fight fight)
        {
            _context.Update(fight);
            return Save();
        }
    }
}
