using CovidCasesList.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CovidCasesList.Controllers
{
    public class HomeController : Controller
    {
        private const string baseUri = "https://api.covid19api.com/";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            CountryCasesModel model = new CountryCasesModel();
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(baseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            using (HttpResponseMessage response = await client.GetAsync(baseUri + "summary"))
            {
                if (response.IsSuccessStatusCode)
                {
                    model = await response.Content.ReadAsAsync<CountryCasesModel>();

                    model.Countries = model.Countries
                        .OrderByDescending(x => x.TotalConfirmed)
                        .Take(20)
                        .ToList();

                    return View(model);
                }
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}