using Ejercicio3PlatziFakeStore.Models.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ejercicio3PlatziFakeStore.Services
{
    public class ApiService
    {
        private string _url = "https://api.escuelajs.co/api/v1/";

        HttpClient client;

        public ApiService()
        {
            client = new HttpClient();
        }


        public async Task<IEnumerable<CategoryDto>> GetCategorias()
        {
            var lista = new List<CategoryDto>();

            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var respuesta = await client.GetAsync($"{_url}categories");

                if (respuesta.IsSuccessStatusCode)
                {
                    respuesta.EnsureSuccessStatusCode();

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();

                    var jsons = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<CategoryDto>>(cuerpo,
                        new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    return jsons;
                }
            }
            return lista;
        }


        public async Task<IEnumerable<ProductoDto>> GetProductos()
        {
            var lista = new List<ProductoDto>();

            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var respuesta = await client.GetAsync($"{_url}products");

                if (respuesta.IsSuccessStatusCode)
                {
                    respuesta.EnsureSuccessStatusCode();

                    var cuerpo = await respuesta.Content.ReadAsStringAsync();

                    var jsons = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ProductoDto>>(cuerpo,
                        new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    return jsons;
                }
            }
            return lista;
        }

        public async Task<bool> CreateProduct(ProductoDto producto)
        {

            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var json = JsonConvert.SerializeObject(producto,
                    new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver()});

                StringContent contenido = new StringContent(json,Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{_url}products/", contenido);

                if (response.IsSuccessStatusCode)
                    return true;
            }
            return false;

        }

        public async Task<bool> DeleteProduct(int id)
        {

            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                HttpResponseMessage response = await client.DeleteAsync($"{_url}products/{id}");
                if (response.IsSuccessStatusCode)
                    return true;
            }
            return false;

        }

        public async Task<IEnumerable<ProductoDto>> FiltrarProductosByCategoria(int idcategoria)
        {
            var lista = new List<ProductoDto>();

            if(Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {

                var respuesta = await client.GetAsync($"{_url}products/?categoryId={idcategoria}");
                if (respuesta.IsSuccessStatusCode)
                {
                    var contenido = JsonConvert.DeserializeObject<IEnumerable<ProductoDto>>
                        (await respuesta.Content.ReadAsStringAsync());
                    return contenido;
                }
            }

            return lista;

        }


        public async Task<bool> EditarProducto(ProductoDto producto)
        {

            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var json = JsonConvert.SerializeObject(producto,
                    new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                StringContent contenido = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"{_url}products/{producto.Id}", contenido);

                if (response.IsSuccessStatusCode)
                    return true;
            }
            return false;

        }




    }
}
