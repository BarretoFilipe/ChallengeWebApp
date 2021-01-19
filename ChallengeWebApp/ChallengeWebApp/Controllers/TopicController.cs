using ChallengeWebApp.Application;
using ChallengeWebApp.Data;
using ChallengeWebApp.Models;
using ChallengeWebApp.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeWebApp.Controllers
{
    [Authorize]
    public class TopicController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TopicController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.IdentityUserId = _userManager.GetUserId(User);
            var topics = await _context.Topics
                            .Include(t => t.IdentityUser)
                            .AsNoTracking()
                            .Select(topic => new TopicViewModel
                            {
                                Id = topic.Id,
                                Title = topic.Title,
                                Description = topic.Description,
                                IdentityUserId = topic.IdentityUserId,
                                IdentityUserName = topic.IdentityUser.UserName,
                                CreationDate = topic.CreationDate.ToString("dd/MM/yyyy HH:mm:ss")
                            }).ToListAsync();

            return View(topics);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTopicCommand topicCommand)
        {
            var validator = new CreateTopicCommandValidator();
            var results = validator.Validate(topicCommand);
            if (!results.IsValid)
            {
                results.AddToModelState(ModelState, null);
                return View();
            }

            var newTopic = new Topic
                (
                    topicCommand.Title,
                    topicCommand.Description,
                    _userManager.GetUserId(User)
                );

            _context.Topics.Add(newTopic);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicCommand = await _context.Topics
                .AsNoTracking()
                .Include(t => t.IdentityUser)
                .Where(m => m.Id == id)
                .Select(topic => new EditTopicCommand
                {
                    Id = topic.Id,
                    Title = topic.Title,
                    Description = topic.Description,
                    IdentityUserId = topic.IdentityUserId,
                }).FirstOrDefaultAsync();

            if (topicCommand == null || topicCommand.IdentityUserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            return View(topicCommand);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTopicCommand topicCommand)
        {
            var validator = new EditTopicCommandValidator();
            var results = validator.Validate(topicCommand);
            if (!results.IsValid)
            {
                results.AddToModelState(ModelState, null);
                return View();
            }

            var topic = await _context.Topics
                .Include(t => t.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == topicCommand.Id);

            if (topic == null)
            {
                return NotFound();
            }

            topic.SetTitle(topicCommand.Title);
            topic.SetDescription(topicCommand.Description);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            ViewBag.IdentityUserId = _userManager.GetUserId(User);
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .AsNoTracking()
                .Include(t => t.IdentityUser)
                .Where(m => m.Id == id)
                .Select(topic => new TopicViewModel
                {
                    Id = topic.Id,
                    Title = topic.Title,
                    Description = topic.Description,
                    IdentityUserId = topic.IdentityUserId,
                    IdentityUserName = topic.IdentityUser.UserName,
                    CreationDate = topic.CreationDate.ToString("dd/MM/yyyy HH:mm:ss")
                }).FirstOrDefaultAsync();

            return View(topic);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid? id)
        {
            var topic = await _context.Topics.FindAsync(id);

            if (topic == null)
            {
                return NotFound();
            }
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}