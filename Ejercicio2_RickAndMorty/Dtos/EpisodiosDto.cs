using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2_RickAndMorty.Dtos
{
   public class EpisodiosDto
    {
        public List<Episodio> Results { get; set; }
    }
    public class Episodio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Air_Date { get; set; }
        public string Episode { get; set; }
        public List<string> Characters { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
    }
}
