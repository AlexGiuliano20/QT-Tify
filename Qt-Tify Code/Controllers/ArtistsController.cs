using System;
using System.Diagnostics;
using System.Threading.Tasks;
using QTtify.Models;
using QTtify.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace QTtify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IConfiguration _configuration;
        private readonly ISpotifyService _spotifyService;

        public ArtistsController( //Constructor
            ISpotifyAccountService spotifyAccountService,
            IConfiguration configuration,
            ISpotifyService spotifyService)
        {
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
            _spotifyService = spotifyService;
        }

        //Artista Ejemplo
        [HttpGet]
        [Route("ejemplo")] //Endpoint: https://localhost:44398/api/Artists/ejemplo
        public async Task<Artist> GetArtist() //Parametros dados del new releases   PARA QUE RECIBA LA ID DE AFUERA, ver min. 1:28:00 de mvc rest api 5 horas
        {
            try
            {
                var token = await _spotifyAccountService.GetToken( //Token de autorización para entrar a la API de spotify
                    _configuration["Spotify:ClientId"],
                    _configuration["Spotify:ClientSecret"]);

                var newArtist = await _spotifyService.GetArtist("6zvul52xwTWzilBZl6BUbT", token); //parametros especificos de la función new releases

                return newArtist;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

                return null; //CAMBIAR QUIZAS
            }
        }

        //Artista Individual por ID
        [HttpGet("{id}")] //endpoint: https://localhost:44398/api/Artists/{id}
        public async Task<Artist> GetArtist(String id) //Parametros dados del new releases   PARA QUE RECIBA LA ID DE AFUERA, ver min. 1:28:00 de mvc rest api 5 horas
        {
            try
            {
                var token = await _spotifyAccountService.GetToken( //Token de autorización para entrar a la API de spotify
                    _configuration["Spotify:ClientId"],
                    _configuration["Spotify:ClientSecret"]);

                var newArtist = await _spotifyService.GetArtist(id, token); //parametros especificos de la función new releases

                return newArtist;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

                return null; //CAMBIAR QUIZAS
            }
        }
    }
}
