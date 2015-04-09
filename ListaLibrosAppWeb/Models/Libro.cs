using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListaLibrosAppWeb.Models
{
    public class Libro
    {
        public int  Id { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public int Edicion { get; set; }
        public Double Precio { get; set; }
        public bool Disponibilidad { get; set; }
    }
}