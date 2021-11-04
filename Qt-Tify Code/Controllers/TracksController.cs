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
        
        //Nuevos lanzamientos de US     FIXEAR LINEA 39 EN SpotifyService (Por ahora funciona pero le cambia el nombre del artista a todos los lanzamientos)
        [HttpGet]
        [Route("newReleases")] //Endpoint: https://localhost:44398/api/Tracks/newReleases
        public async Task<IEnumerable<Release>> GetReleases() //Parametros dados del new releases
        {
            try
            {
                var token = await _spotifyAccountService.GetToken( //Token de autorización para entrar a la API de spotify
                    _configuration["Spotify:ClientId"],
                    _configuration["Spotify:ClientSecret"]);

                var newReleases = await _spotifyService.GetNewReleases("US", 20, token); //parametros de ejemplo

                return newReleases;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

                return Enumerable.Empty<Release>(); //Devuelve conjunto vacío
            }
        }

        //Nuevos lanzamientos especificando pais
        [HttpGet]
        [Route("newReleases/{pais}")] //Endpoint: https://localhost:44398/api/Tracks/newReleases/{pais}  //EJEMPLOS   newReleases/AR   newReleases/US   newReleases/ES  
        public async Task<IEnumerable<Release>> GetReleasesPais(String pais) //Parametros dados del new releases
        {
            try
            {
                var token = await _spotifyAccountService.GetToken( //Token de autorización para entrar a la API de spotify
                    _configuration["Spotify:ClientId"],
                    _configuration["Spotify:ClientSecret"]);

                var newReleases = await _spotifyService.GetNewReleases(pais, 20, token); //parametros de ejemplo

                return newReleases;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

                return Enumerable.Empty<Release>(); //Devuelve conjunto vacío
            }
        }


    }
}