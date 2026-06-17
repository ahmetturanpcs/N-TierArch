using exampleProject.Core.Entities;
using exampleProject.Core.Interfaces;
using exampleProject.Web.Models;
using exampleProject.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace exampleProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Sadece arayüze (sözleşmeye) bağımlıyız, Data katmanını doğrudan çağırmıyoruz!
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<Store> _storeRepository;
        public HomeController(ILogger<HomeController> logger, IGenericRepository<Category> categoryRepository, IGenericRepository<Store> storeRepository)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _storeRepository = storeRepository;
        }

        public async Task<IActionResult> Index()
        {
            
            var categories = await _categoryRepository.GetAllAsync();
            var stores = await _storeRepository.GetAllAsync();

            var viewModel = new DashboardViewModel
            {
                Categories = categories,
                TotalCategoryCount = categories.Count,
                ActiveCategoryCount = categories.Count(c => c.IsActive),

                Stores = stores,
                TotalStoreCount = stores.Count, 
                ActiveStoreCount = stores.Count(c => c.IsActive)
            };

            return View(viewModel);
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
