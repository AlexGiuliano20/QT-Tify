using QTtify.Models;
using QTtify.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QTtify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IConfiguration _configuration;
        private readonly ISpotifyService _spotifyService;

        public TracksController( //Constructor
            ISpotifyAccountService spotifyAccountService,
            IConfiguration configuration,
            ISpotifyService spotifyService)
        {
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
            _spotifyService = spotifyService;
        }
        
        //Nuevos lanzamientos
        [HttpGet]
        [Route("newReleases")]
        public async Task<IEnumerable<Release>> GetReleases() //Parametros dados del new releases
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
        

    }
}