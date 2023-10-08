namespace FightClubWebApp.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int? Win{ get; set; }
        public int? Lost { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
