using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System.Drawing;
using Tamagotchi.Data;
using Tamagotchi.Areas.Admin.Controllers;
using Hangfire;

namespace Tamagotchi.Controllers
{
    public class GameController : AuthorizeBaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public GameController(ApplicationDbContext dbContext, IMapper mapper, IBackgroundJobClient backgroundJobClient)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _backgroundJobClient = backgroundJobClient;
        }

        public IActionResult Index()
        {
            string currentUserName = User.Identity.Name;

            bool hasCurrentTamagotchi = _dbContext.CurrentTamagotchis.Any(ct => ct.Id_User == currentUserName);

            if (hasCurrentTamagotchi)
            {
                var currentTamagotchi = _dbContext.CurrentTamagotchis
                    .FirstOrDefault(m => m.Id_User == currentUserName);

                if (currentTamagotchi.Energy <= 0 || currentTamagotchi.Health <= 0 || currentTamagotchi.Fun <= 0 || currentTamagotchi.Hygiene <= 0 || currentTamagotchi.Hunger <= 0)
                {
                    return RedirectToAction("TamagotchiDead", new { tamagotchiId = currentTamagotchi.Id });
                }

                return View("Game", currentTamagotchi);
            }
            else
            {
                return RedirectToAction("ChoosePet");
            }
        }

        public IActionResult Game()
        {
            return View();
        }

        public IActionResult TamagotchiDead(int tamagotchiId)
        {
            var tamagotchiToDelete = _dbContext.CurrentTamagotchis.FirstOrDefault(ct => ct.Id == tamagotchiId);

            if (tamagotchiToDelete != null)
            {
                _dbContext.CurrentTamagotchis.Remove(tamagotchiToDelete);
                _dbContext.SaveChangesAsync();
            }

            RecurringJob.RemoveIfExists($"DecreaseStats_{tamagotchiId}");

            return View();
        }

        public IActionResult Fruit(int? id)
        {
            ViewBag.TamagotchiId = id;
            return View();
        }

        public IActionResult TTT(int? id)
        {
            ViewBag.TamagotchiId = id;
            return View();
        }

        public IActionResult Srp(int? id)
        {
            ViewBag.TamagotchiId = id;
            return View();
        }

        public IActionResult AddFun(int? id, string win)
        {
            CurrentTamagotchi currentTamagotchi = _dbContext.CurrentTamagotchis.FirstOrDefault(s => s.Id == id);

            if (win == "win")
            {
                if (currentTamagotchi.Fun + 10 <= currentTamagotchi.Max_Fun)
                {
                    currentTamagotchi.Fun += 10;
                    _dbContext.Update(currentTamagotchi);
                    _dbContext.SaveChangesAsync();
                }
                else
                {
                    var max = currentTamagotchi.Max_Fun;
                    currentTamagotchi.Fun = max;
                    _dbContext.Update(currentTamagotchi);
                    _dbContext.SaveChangesAsync();
                }
            }
            return View("Game", currentTamagotchi);
        }

        public async Task<IActionResult> ChoosePet()
        {
            string currentUserName = User.Identity.Name;

            bool hasCurrentTamagotchi = _dbContext.CurrentTamagotchis.Any(ct => ct.Id_User == currentUserName);

            if (hasCurrentTamagotchi)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return _dbContext.Pets != null ?
                View(await _dbContext.Pets.ToListAsync()) :
                Problem("Entity set 'PetsContext.Pets'  is null.");
            }
        }

        public IActionResult SelectPet(int? id, string gender)
        {

            if (id == null || _dbContext.Pets == null)
            {
                return NotFound();
            }

            Pets Pet = _dbContext.Pets.FirstOrDefault(p => p.Id == id);

            if (Pet == null)
            {
                return NotFound();
            }
            Statistics statistics = _dbContext.Statistics.FirstOrDefault(s => s.Id == Pet.Id_Stat);
            Pet.Statistics = statistics;

            CurrentTamagotchi currentTamagotchi = _mapper.Map<CurrentTamagotchi>(Pet);
            currentTamagotchi.Gender = gender;
            currentTamagotchi.Id_User = User.Identity.Name;
            _dbContext.CurrentTamagotchis.Add(currentTamagotchi);
            _dbContext.SaveChanges();

            var idneed = _dbContext.CurrentTamagotchis
                    .FirstOrDefault(m => m.Id_User == User.Identity.Name);

            RecurringJob.AddOrUpdate($"DecreaseStats_{idneed.Id}", () => DecreaseStats(idneed.Id), Cron.HourInterval(2));

            return RedirectToAction("Index"); 
        }

        public IActionResult FeedPet(int id)
        {
            CurrentTamagotchi currentTamagotchi = _dbContext.CurrentTamagotchis.FirstOrDefault(s => s.Id == id);
            
            if (currentTamagotchi.Hunger + 10 <= currentTamagotchi.Max_Hunger)
            {   
                currentTamagotchi.Hunger += 10;
                _dbContext.Update(currentTamagotchi);
                _dbContext.SaveChangesAsync();
            }
            else
            {
                var max = currentTamagotchi.Max_Hunger;
                currentTamagotchi.Hunger = max;
                _dbContext.Update(currentTamagotchi);
                _dbContext.SaveChangesAsync();
            }
                
            return View("Game", currentTamagotchi);
        }

        public IActionResult CleanPet(int id)
        {
            CurrentTamagotchi currentTamagotchi = _dbContext.CurrentTamagotchis.FirstOrDefault(s => s.Id == id);

            if (currentTamagotchi.Hygiene + 50 <= currentTamagotchi.Max_Hygiene)
            {
                currentTamagotchi.Hygiene += 50;
                _dbContext.Update(currentTamagotchi);
                _dbContext.SaveChangesAsync();
            }
            else
            {
                var max = currentTamagotchi.Max_Hygiene;
                currentTamagotchi.Hygiene = max;
                _dbContext.Update(currentTamagotchi);
                _dbContext.SaveChangesAsync();
            }

            return View("Game", currentTamagotchi);
        }

        public IActionResult CurePet(int id)
        {
            CurrentTamagotchi currentTamagotchi = _dbContext.CurrentTamagotchis.FirstOrDefault(s => s.Id == id);

            if (currentTamagotchi.Health + 50 <= currentTamagotchi.Max_Health)
            {
                currentTamagotchi.Health += 50;
                _dbContext.Update(currentTamagotchi);
                _dbContext.SaveChangesAsync();
            }
            else
            {
                var max = currentTamagotchi.Max_Health;
                currentTamagotchi.Health = max;
                _dbContext.Update(currentTamagotchi);
                _dbContext.SaveChangesAsync();
            }

            return View("Game", currentTamagotchi);
        }

        public IActionResult SleepPet(int id, string ageState)
        {
            CurrentTamagotchi currentTamagotchi = _dbContext.CurrentTamagotchis.FirstOrDefault(s => s.Id == id);

            currentTamagotchi.Age_State = "true";
            _dbContext.Update(currentTamagotchi);
            _dbContext.SaveChangesAsync();

            if (currentTamagotchi != null)
            {
                RecurringJob.AddOrUpdate($"IncreaseSleep_{id}", () => IncreaseSleep(id), Cron.MinuteInterval(1));
            }

            return View("Game", currentTamagotchi);
        }

        public IActionResult StopSleepPet(int id, string ageState)
        {
            CurrentTamagotchi currentTamagotchi = _dbContext.CurrentTamagotchis.FirstOrDefault(s => s.Id == id);

            currentTamagotchi.Age_State = "false";
            _dbContext.Update(currentTamagotchi);
            _dbContext.SaveChangesAsync();

            RecurringJob.RemoveIfExists($"IncreaseSleep_{id}");

            return View("Game", currentTamagotchi);
        }

        [AutomaticRetry(Attempts = 0)]
        public void IncreaseSleep(int id)
        {

            CurrentTamagotchi currentTamagotchi = _dbContext.CurrentTamagotchis.FirstOrDefault(s => s.Id == id);

            if (currentTamagotchi != null)
            {
                if (currentTamagotchi.Energy + 10 <= currentTamagotchi.Max_Energy)
                {
                    currentTamagotchi.Energy += 10;
                    _dbContext.Update(currentTamagotchi);
                    _dbContext.SaveChangesAsync();
                }
                else
                {
                    var max = currentTamagotchi.Max_Energy;
                    currentTamagotchi.Energy = max;
                    _dbContext.Update(currentTamagotchi);
                    _dbContext.SaveChangesAsync();
                }
            }
        }

        [AutomaticRetry(Attempts = 0)]
        public void DecreaseStats(int? id)
        {

            CurrentTamagotchi currentTamagotchi = _dbContext.CurrentTamagotchis.FirstOrDefault(s => s.Id == id);

            if (currentTamagotchi != null)
            {
                if (currentTamagotchi.Energy - 5 > 0)
                {
                    currentTamagotchi.Energy -= 5;
                }
                else
                {
                    currentTamagotchi.Energy = 0;
                }

                if (currentTamagotchi.Fun - 10 > 0)
                {
                    currentTamagotchi.Fun -= 10;
                }
                else
                {
                    currentTamagotchi.Fun = 0;
                }

                if (currentTamagotchi.Hygiene - 10 > 0)
                {
                    currentTamagotchi.Hygiene -= 10;
                }
                else
                {
                    currentTamagotchi.Hygiene = 0;
                }

                if (currentTamagotchi.Health - 10 > 0)
                {
                    currentTamagotchi.Health -= 10;
                }
                else
                {
                    currentTamagotchi.Health = 0;
                }

                if (currentTamagotchi.Hunger - 10 > 0)
                {
                    currentTamagotchi.Hunger -= 10;
                }
                else
                {
                    currentTamagotchi.Hunger = 0;
                }

                _dbContext.Update(currentTamagotchi);
                _dbContext.SaveChanges();
            }
        }
    }
}
