using QTtify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace QTtify.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly HttpClient _httpClient;

        public SpotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //GET NUEVOS LANZAMIENTOS
        public async Task<IEnumerable<Release>> GetNewReleases(string countryCode, int limit, string accessToken) //Funcion que devuelve los nuevos lanzamientos. Los mismos parametros de ISpotifyService
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken); //autorización con token (hay que poner siempre en cada funcion)

            var response = await _httpClient.GetAsync($"browse/new-releases?country={countryCode}&limit={limit}"); //petición http, ni puta idea. Supongo que varía con cada función
            response.EnsureSuccessStatusCode(); //Chequea o valida la petición

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<GetNewReleaseResult>(responseStream); //Llama a la clase de la carpeta Models

            return responseObject?.albums?.items.Select(i => new Release //Retorna a la función un conjunto de canciones con la data
            {
                Name = i.name,
                Date = i.release_date,
                ImageUrl = i.images.FirstOrDefault().url,
                Link = i.external_urls.spotify,
                Artists = "SI JAJA"
               // Artists = string.Join(",", i.artists_newRelease.Select(i => i.name)) //Error en esta linea
            });
        }

        
        //GET ARTISTA
        public async Task<Artist> GetArtist(string id, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"artists?/id={id}"); //por esto debe ser que no funciona
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<GetArtistResult>(responseStream);

            Artist artista = new Artist {
                Name = responseObject.root.name,
                ImageUrl = responseObject.root.images.FirstOrDefault().url,
                Link = responseObject.root.external_urls.spotify
            };
           

            return artista;

        }
    }
}
