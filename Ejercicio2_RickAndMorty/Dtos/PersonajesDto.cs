﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2_RickAndMorty.Dtos
{
    public class PersonajesDto
    {
        public IEnumerable<PersonajeDto> Results { get; set; }
    }
    public class PersonajeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public Origin Origin { get; set; }
        public Location Location { get; set; }
        public string Image { get; set; }
        public List<string> Episode { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
    }
    public class Location
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class Origin
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
