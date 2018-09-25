using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NASAPicture.Helpers;
using Newtonsoft.Json;

namespace NASAPicture.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetNASAPicture()
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY"))
            using (HttpContent content = response.Content)
            {
                // ... Read the string.
                var nasaResponse = await content.ReadAsStringAsync().ConfigureAwait(false);

                // ... Display the result.
                var rst = JsonConvert.DeserializeObject<NasaResponse>(nasaResponse);
                return await Task.Run(() => View(rst));
            }           
        }
    }
}