using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2_RickAndMorty.Dtos
{
    public class UbicacionesDto
    {
        public List<Ubicacion> Results { get; set; }
    }

    public class Ubicacion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Dimension { get; set; }
        public List<string> Residents { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
    }

}
