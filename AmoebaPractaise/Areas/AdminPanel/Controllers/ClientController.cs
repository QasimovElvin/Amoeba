using Amoeba.DAL;
using Amoeba.Models;
using Amoeba.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amoeba.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ClientController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async  Task<IActionResult> Index()
        {

            var clients = await _context.clients.Include(x => x.profession).ToListAsync();
            return View(clients);
        }
        public IActionResult Create()
        {
            ViewBag.Profession = _context.professions.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            ViewBag.Profession = _context.professions.ToList();
            if(!ModelState.IsValid || client.ImageFile==null)return View();
            if (!client.ImageFile.CheckType("image/"))
            {
                ModelState.AddModelError("", "Image Type Incorrect");
                return View();
            }
            if (client.ImageFile.CheckSize(500))
            {
                ModelState.AddModelError("", "Image Size Wrong");
                return View();
            }
             client.Image = await client.ImageFile.SaveFileAsync(_env.WebRootPath,"img");
             await _context.AddAsync(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Profession = _context.professions.ToList();
            Client? exists =await _context.clients.Include(x=>x.profession).FirstOrDefaultAsync(x => x.Id == id);
            return View(exists);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Client client)
        {
            ViewBag.Profession = _context.professions.ToList();
            Client? exists = await _context.clients.Include(x => x.profession).FirstOrDefaultAsync(x => x.Id == client.Id);
            if (exists == null)
            {
                ModelState.AddModelError("", "Client is null");
                return View();
            }
            if (client.ImageFile != null)
            {
                if (!client.ImageFile.CheckType("image/"))
                {
                    ModelState.AddModelError("", "Image Type Incorrect");
                    return View();
                }
                if (client.ImageFile.CheckSize(500))
                {
                    ModelState.AddModelError("", "Image Size Wrong");
                    return View();
                }
                string path = Path.Combine(_env.WebRootPath,"assets",exists.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                exists.Image = await client.ImageFile.SaveFileAsync(_env.WebRootPath, "img");
            }
                exists.Name = client.Name;
                exists.Description = client.Description;
                exists.ProfessionId=client.ProfessionId;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Client? exists = await _context.clients.FirstOrDefaultAsync(x => x.Id == id);
            if (exists == null)
            {
                ModelState.AddModelError("", "Client is null");
                return View();
            }
            string path = Path.Combine(_env.WebRootPath, "assets", exists.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.clients.Remove(exists);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
