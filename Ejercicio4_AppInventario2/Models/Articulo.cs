using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio4_AppInventario2.Models
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Exitencia { get; set; }

    }
}
