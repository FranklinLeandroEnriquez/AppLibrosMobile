using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrosMobile.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        
        public string Editorial { get; set; }
        public int Anio { get; set; }    
        
        public int Paginas { get; set; }
        public int GeneroId { get; set; }
        public Genre Genero { get; set; }
    }
}
