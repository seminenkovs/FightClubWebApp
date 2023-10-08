using CloudinaryDotNet.Actions;
using FightClubWebApp.Data;
using FightClubWebApp.Interfaces;
using FightClubWebApp.Models;
using FightClubWebApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using FightClubWebApp.ViewModels;

namespace FightClubWebApp.Controllers
{
    public class FightController : Controller
    {
        private readonly IFightRepository _fightRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _contextAccessor;

        public FightController(IFightRepository fightRepository, IPhotoService photoService, IHttpContextAccessor contextAccessor)
        {
            _fightRepository = fightRepository;
            _photoService = photoService;
            _contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            var fights = await _fightRepository.GetAll();
            return View(fights);
        }

        public async Task<ActionResult> Detail(int id)
        {
            var fight = await _fightRepository.GetByIdAsync(id);
            return View(fight);
        }

        public IActionResult Create()
        {
            var currentUserId = _contextAccessor.HttpContext?.User.GetUserId();
            var createRaceViewModel = new CreateFightViewModel { AppUserId = currentUserId };
            return View(createRaceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFightViewModel fightVm)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(fightVm.Image);

                var fight = new Fight
                {
                    Title = fightVm.Title,
                    Description = fightVm.Description,
                    Image = result.Url.ToString(),
                    AppUserId = fightVm.AppUserId,
                    Address = new Address
                    {
                        City = fightVm.Address.City,
                        Street = fightVm.Address.Street,
                        State = fightVm.Address.State
                    }
                };
                _fightRepository.Add(fight);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(fightVm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var fight = await _fightRepository.GetByIdAsync(id);
            if (fight == null) return View("Error");
            var raceVM = new EditFightViewModel()
            {
                Title = fight.Title,
                Description = fight.Description,
                FightCategory = fight.FightCategory,
                URL = fight.Image,
                AddressId = fight.AddressId,
                Address = fight.Address
            };

            return View(raceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditFightViewModel fightVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", fightVM);
            }

            var userRace = await _fightRepository.GetByIdAsyncNoTracking(id);
            if (userRace != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userRace.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(fightVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(fightVM.Image);

                var fight = new Fight
                {
                    Id = id,
                    Title = fightVM.Title,
                    Description = fightVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = fightVM.AddressId,
                    Address = fightVM.Address
                };

                _fightRepository.Update(fight);

                return RedirectToAction("Index");
            }
            else
            {
                return View(fightVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var raceDetails = await _fightRepository.GetByIdAsync(id);
            if (raceDetails == null) return View("Error");
            return View(raceDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var raceDetails = await _fightRepository.GetByIdAsync(id);
            if (raceDetails == null) return View("Error");

            _fightRepository.Delete(raceDetails);

            return RedirectToAction("Index");
        }
    }
}
