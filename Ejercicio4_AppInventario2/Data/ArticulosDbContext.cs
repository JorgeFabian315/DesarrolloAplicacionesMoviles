
using System.Collections.Generic;
using Ejercicio4_AppInventario2.Models;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace Ejercicio4_AppInventario2.Data
{
    public class ArticulosDbContext
    {
        private const string _nombrebase = "articulos.db";
        private static string dbFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), _nombrebase);
        private string _connectionString = DeviceInfo.Platform == DevicePlatform.Android
            ? $"Data Source={dbFolder}"
            : $"Data Source={_nombrebase}";


        public ArticulosDbContext()
        {

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    var command = connection.CreateCommand();
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS articulos(
                                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                        descripcion VARCHAR(60) NOT NULL,
                                        precio DECIMAL NOT NULL,
                                        existencia INTEGER NOT NULL
                                        );";
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task<bool> Agregar(Articulo articulo)
        {

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();

                command.CommandText = @"INSERT INTO articulos (descripcion,precio,existencia) VALUES ($descripcion,$precio, $existencia)";

                command.Parameters.AddWithValue("$descripcion", articulo.Descripcion);
                command.Parameters.AddWithValue("$precio", articulo.Precio);
                command.Parameters.AddWithValue("$existencia", articulo.Exitencia);

                return await command.ExecuteNonQueryAsync() > 0;
            }

        }
        //C.R.U.D
        public async Task<Articulo> GetById(int id)
        {
            Articulo articulo = null;

            if (id <= 0)
                throw new ArgumentException("El id no es mayor a 0");

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM articulos WHERE id = $id";
                command.Parameters.AddWithValue("$id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {

                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        articulo = new Articulo()
                        {
                            Id = reader.GetInt32(0),
                            Descripcion = reader.GetString(1),
                            Precio = reader.GetDecimal(2),
                            Exitencia = reader.GetInt32(3),
                        };
                    }

                }
            }
            return articulo;
        }
        public async Task<IEnumerable<Articulo>> GetAll()
        {

            List<Articulo> articulosList = new List<Articulo>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT id,descripcion,precio,existencia FROM articulos";

                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    articulosList.Add(new Articulo
                    {
                        Id = reader.GetInt32(0),
                        Descripcion = reader.GetString(1),
                        Precio = reader.GetDecimal(2),
                        Exitencia = reader.GetInt32(3),
                    });
                }
            }

            return articulosList;
        }

        public async Task<bool> Editar(Articulo articulo)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"UPDATE articulos SET descripcion = $descripcion,precio = $precio ,existencia = $existencia
                                            WHERE id = $id";

                command.Parameters.AddWithValue("$id", articulo.Id);
                command.Parameters.AddWithValue("$descripcion", articulo.Descripcion);
                command.Parameters.AddWithValue("$precio", articulo.Precio);
                command.Parameters.AddWithValue("$existencia", articulo.Exitencia);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"DELETE FROM articulos WHERE id = $id";

                command.Parameters.AddWithValue("$id", id);

                var r = await command.ExecuteNonQueryAsync();
                return r > 0;
            }
        }
        public async Task<bool> EliminarTodos()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"DELETE FROM articulos";
                var r = await command.ExecuteNonQueryAsync();
                return r > 0;
            }
        }
    }
}
