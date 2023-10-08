using FightClubWebApp.Data.Enum;
using FightClubWebApp.Models;
using FightClubWebApp.Data.Enum;
using FightClubWebApp.Models;

namespace FightClubWebApp.ViewModels
{
    public class EditCLubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        public ClubCategory ClubCategory { get; set; }
    }
}
