using System;
using System.Collections.Generic;
using System.Text;

namespace VideoClub
{
    class Alquiler:BBDD
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdPelicula { get; set; }
        public string FechaAlquiler { get; set; }
        public string FechaDevolucion { get; set; }
        public string Devuelta { get; set; }

        public Alquiler(int idUsuario, int idPelicula, string fechaAlquiler, string fechaDevolucion, string devuelta)
        {
            IdUsuario = idUsuario;
            IdPelicula = idPelicula;
            FechaAlquiler = fechaAlquiler;
            FechaDevolucion = fechaDevolucion;
            Devuelta = devuelta;
        }
    }
}
