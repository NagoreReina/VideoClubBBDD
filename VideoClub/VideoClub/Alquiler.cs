using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace VideoClub
{
    class Alquiler
    {
        //CONEXION CON LA BASE DE DATOS
        static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-C1JLP92\\SQLEXPRESS;Initial Catalog=VideoClub;Integrated Security=True");
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
        //Función para hacer consultas con la base de datos
        public bool ConsultarBase(string query)
        {
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    connection.Close();
                    return true;
                }
                connection.Close();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: NO SE HA PODIDO CONSULTAR LA BASE DE DATOS");
                Console.ForegroundColor = ConsoleColor.White;
            }
            return false;

        }

        //funcion para modificar en la base de datos
        public void ModificarBase(string query)
        {
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: NO SE HA PODIDO MODIFICAR LA BASE DE DATOS");
                Console.ForegroundColor = ConsoleColor.White;
            }

        }
    }
}
