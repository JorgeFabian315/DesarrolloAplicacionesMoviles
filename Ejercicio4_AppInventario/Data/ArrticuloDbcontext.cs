using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio4_AppInventario.Models;
using Microsoft.Data.Sqlite;

namespace Ejercicio4_AppInventario.Data
{
    public class ArrticuloDbcontext
    {
        private const string _connectionString = "Data Source=articulos.db";
        public ArrticuloDbcontext()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"CREATE IF NOT EXIST articulos(
                                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                        descripcion VARCHAR(60) NOT NULL,
                                        precio DECIMAL NOT NULL,
                                        existencia INTEGER NOT NULL
                                        );";
                command.ExecuteNonQuery();
            }
        }


        public async Task Agregar(Articulo articulo)
        {

            using(var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                
                command.CommandText = @"INSERT INTO articulos (descripcion,precio,existencia) VALUES ($descripcion,$precio, $existencia)";

                command.Parameters.AddWithValue("$descripcion", articulo.Descripcion);
                command.Parameters.AddWithValue("$precio", articulo.Precio);
                command.Parameters.AddWithValue("$existencia", articulo.Exitencia);

                await command.ExecuteNonQueryAsync();
            }



        }

    }
}
