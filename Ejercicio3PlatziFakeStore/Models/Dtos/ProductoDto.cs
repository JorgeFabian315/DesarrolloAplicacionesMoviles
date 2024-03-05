using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio3PlatziFakeStore.Models.Dtos
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public DateTime CreationAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CategoryDto Category { get; set; } = new();
        public int CategoryId { get; set; }
    }
}
