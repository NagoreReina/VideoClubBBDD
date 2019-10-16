using System;
using System.Collections.Generic;
using System.Text;

namespace VideoClub
{
    class Pelicula : BBDD
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Disponible { get; set; }
        public int EdadRecomendada { get; set; }

        public Pelicula(string nombre, string descripcion, string disponible, int edadRecomendada)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Disponible = disponible;
            EdadRecomendada = edadRecomendada;
        }
    }
}
