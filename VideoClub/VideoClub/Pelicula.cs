using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace VideoClub
{
    class Pelicula : IConsultasYModificaciones
    {
        //CONEXION CON LA BASE DE DATOS
        static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-C1JLP92\\SQLEXPRESS;Initial Catalog=VideoClub;Integrated Security=True");
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Disponible { get; set; }
        public int EdadRecomendada { get; set; }
        public int Stock { get; set; }

        public Pelicula(int id, string nombre, string descripcion, string disponible, int edadRecomendada, int stock)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Disponible = disponible;
            EdadRecomendada = edadRecomendada;
            Stock = stock;
        }
        public Pelicula()
        {

        }

        public List<Pelicula> AlmacenarPeliculas()
        {
            string query = $"SELECT * FROM Peliculas";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Close();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Pelicula> tempList = new List<Pelicula>();
            while (reader.Read())
            {
                tempList.Add(new Pelicula(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(),reader[3].ToString(), Convert.ToInt32(reader[4].ToString()), Convert.ToInt32(reader[5].ToString())));
            }
            connection.Close();
            return tempList;
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
