using ConsumeSpotifyWebAPI.Models;
using ConsumeSpotifyWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumeSpotifyWebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IConfiguration _configuration;
        private readonly ISpotifyService _spotifyService;

        public HomeController(
            ISpotifyAccountService spotifyAccountService,
            IConfiguration configuration,
            ISpotifyService spotifyService)
        {
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
            _spotifyService = spotifyService;
        }

        public async Task<IActionResult> Index()
        {
            var newReleases = await GetReleases();

            return View(newReleases); //Lo manda y se lo deja picando al frontend
        }

        private async Task<IEnumerable<Release>> GetReleases() //Parametros dados del new releases
        {
            try
            {
                var token = await _spotifyAccountService.GetToken( //Token de autorización para entrar a la API de spotify
                    _configuration["Spotify:ClientId"],
                    _configuration["Spotify:ClientSecret"]);

                var newReleases = await _spotifyService.GetNewReleases("US", 20, token); //parametros especificos de la función new releases

                return newReleases;
            }
            catch (Exception ex) //En caso de que no pueda ejecutar la función
            {
                Debug.Write(ex);

                return Enumerable.Empty<Release>(); //Devuelve conjunto vacío
            }
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
