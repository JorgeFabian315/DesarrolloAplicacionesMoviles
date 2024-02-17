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
        private string url = "https://rickandmortyapi.com/api/character";

        HttpClient client;
        public RickAndMortyServices()
        {
            client = new HttpClient();
        }

        public async Task<List<PersonajeDto>> GetAll()
        {

            var respuesta = await client.GetAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {
                respuesta.EnsureSuccessStatusCode();
                var contenido = await respuesta.Content.ReadAsStringAsync();
                var personajes = Newtonsoft.Json.JsonConvert.DeserializeObject<PersonajesDto>(contenido);
                return personajes.Results;
            }

            return new PersonajesDto().Results;
        }




    }
}
