using exampleProject.Core.Entities;
using exampleProject.Core.Interfaces;
using exampleProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace exampleProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Sadece arayüze (sözleşmeye) bağımlıyız, Data katmanını doğrudan çağırmıyoruz!
        private readonly IGenericRepository<Category> _categoryRepository;
        public HomeController(ILogger<HomeController> logger, IGenericRepository<Category> categoryRepository)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            // Veritabanındaki tüm kategorileri asenkron olarak çekiyoruz
            var categories = await _categoryRepository.GetAllAsync();

            // Veriyi View katmanına gönderiyoruz
            return View(categories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
