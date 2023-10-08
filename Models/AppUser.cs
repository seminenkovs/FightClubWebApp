using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FightClubWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public int? Win { get; set; }
        public int? Lost { get; set; }
        [ForeignKey("Address")]
        public int? AddressID { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Fight> Fights { get; set; }
    }
}