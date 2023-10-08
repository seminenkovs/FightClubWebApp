using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace FightClubWebApp.Models
{
    public class Address
    {
        [Key] 
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
