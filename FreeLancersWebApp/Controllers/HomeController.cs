using Freelancer_Management_API.Model;
using FreeLancersWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace FreeLancersWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7104/");
        }

        #region IndexMainPage
        public async Task<IActionResult> Index()
        {
            try
            {
                var freelancers = await _httpClient.GetFromJsonAsync<List<MC_FreeLancer>>("GetFreeLancer");
                return View(freelancers);
            }
            catch (Exception ex)
            {
                // Log the error
                ViewBag.Error = "Error fetching freelancers: " + ex.Message;
                return View(new List<MC_FreeLancer>());
            }
        }
        #endregion

        #region Insert
        [HttpGet]
        public IActionResult insertFreeLancer()
        {
            try
            {
                return View(new MC_FreeLancer());
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error Creating freelancer: {ex.Message}";
                return View("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> insertFreeLancer(MC_FreeLancer model)
        {
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsJsonAsync($"InsertFreeLancer", model);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Freelancer Create successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Failed to Create freelancer";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error updating freelancer: {ex.Message}";
                return View("Details", model);
            }
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> updateFreeLancer(int id)
        {
            try
            {
                var freelancer = await _httpClient.GetFromJsonAsync<MC_FreeLancer>($"GetFreeLancer/{id}");
                return View(freelancer);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error fetching freelancer: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> updateFreeLancer(MC_FreeLancer model)
        {
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsJsonAsync($"UpdateFreeLancer/{model.FL_Id}", model);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Freelancer updated successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Failed to update freelancer";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error updating freelancer: {ex.Message}";
                return View("Details", model);
            }
        }
        #endregion

        #region delete
        [HttpPost]
        public async Task<IActionResult> deleteFreeLancer(int PN_FL_Id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"deleteFreeLancer/{PN_FL_Id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Freelancer Delete successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Failed to Delete freelancer";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error updating freelancer: {ex.Message}";
                return View("Details", PN_FL_Id);
            }
        }
        #endregion

        #region Others
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}