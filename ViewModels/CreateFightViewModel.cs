using FightClubWebApp.Models;
using FightClubWebApp.Data.Enum;


namespace FightClubWebApp.ViewModels
{
    public class CreateFightViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public FightCategory FightCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
