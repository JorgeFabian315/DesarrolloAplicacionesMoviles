using Ejercicio2_RickAndMorty.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ejercicio2_RickAndMorty.Services
{
    public class RickAndMortyServices
    {
        private string url = "https://rickandmortyapi.com/api/";

        HttpClient client;
        public RickAndMortyServices()
        {
            client = new HttpClient();
        }

        public async Task<IEnumerable<PersonajeDto>> GetAllCharacters()
        {

            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {

                var respuesta = await client.GetAsync($"{url}character");

                if (respuesta.IsSuccessStatusCode)
                {
                    respuesta.EnsureSuccessStatusCode();
                    var contenido = await respuesta.Content.ReadAsStringAsync();
                    var personajes = Newtonsoft.Json.JsonConvert.DeserializeObject<PersonajesDto>(contenido);
                    return personajes.Results;
                }
            }
            
            return new PersonajesDto().Results;
        }

        public async Task<List<Ubicacion>> GetAllLocations()
        {
            List<Ubicacion> ubicaciones = new();

            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                HttpResponseMessage respuesta = await client.GetAsync($"{url}location");

                if (respuesta.IsSuccessStatusCode)
                {
                    respuesta.EnsureSuccessStatusCode();

                    string contenido = await respuesta.Content.ReadAsStringAsync();

                    ubicaciones = System.Text.Json.JsonSerializer.Deserialize<UbicacionesDto>(contenido,
                             new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }).Results;

                    return ubicaciones;
                }
            }
            return ubicaciones;
        }

        public async Task<List<Episodio>> GetAllEpisodes()
        {
            List<Episodio> episodios = new();

            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                HttpResponseMessage respuesta = await client.GetAsync($"{url}episode");

                if (respuesta.IsSuccessStatusCode)
                {
                    respuesta.EnsureSuccessStatusCode();

                    string contenido = await respuesta.Content.ReadAsStringAsync();

                    episodios = System.Text.Json.JsonSerializer.Deserialize<EpisodiosDto>(contenido,
                             new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }).Results;

                    return episodios;
                }
            }
            return episodios;
        }
    }
}
