using CloudinaryDotNet.Actions;
using FightClubWebApp.Interfaces;
using FightClubWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using FightClubWebApp.Data;
using FightClubWebApp.Models;
using FightClubWebApp.Services;
using FightClubWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;


        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashboardRepository.GetAllUserRaces();
            var userClubs = await _dashboardRepository.GetAllUserCLubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Fights = userRaces,
                Clubs = userClubs
            };

            return View(dashboardViewModel);
        }
    }
}
