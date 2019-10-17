using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace VideoClub
{
    class Alquiler :IConsultasYModificaciones
    {
        //CONEXION CON LA BASE DE DATOS
        static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-C1JLP92\\SQLEXPRESS;Initial Catalog=VideoClub;Integrated Security=True");
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdPelicula { get; set; }
        public string FechaAlquiler { get; set; }
        public string FechaDevolucion { get; set; }
        public string Devuelta { get; set; }

        public Alquiler(int id,int idUsuario, int idPelicula, string fechaAlquiler, string fechaDevolucion, string devuelta)
        {
            Id = id;
            IdUsuario = idUsuario;
            IdPelicula = idPelicula;
            FechaAlquiler = fechaAlquiler;
            FechaDevolucion = fechaDevolucion;
            Devuelta = devuelta;
        }
        public Alquiler() { }
        public bool GenerarAlquiler(int idUsuario, int idPelicula)
        {
            string query = $"INSERT INTO Alquiler(IdUsuario, IdPelicula, FechaAlquiler, Devuelta) VALUES ({idUsuario},{idPelicula},'{DateTime.Today.ToString("MM/dd/yyyy")}', 'NO')";
            ModificarBase(query);
            return true;
        }
        public void DevolverPelicula(int idAlquiler)
        {
            string query = $"UPDATE Alquiler Set Devuelta = 'SI',FechaDevolucion = '{DateTime.Today.ToString("MM/dd/yyyy")}' WHERE Id = { idAlquiler }";
            ModificarBase(query);
        }
        public List<Alquiler> AlquileresDeUsuario(int idUsuario)
        {
            List<Alquiler> alquileresUsuario = new List<Alquiler>();
            string query = $"SELECT * FROM Alquiler WHERE IdUsuario = {idUsuario} AND Devuelta = 'NO'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Close();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                alquileresUsuario.Add(new Alquiler(Convert.ToInt32(reader[0].ToString()), Convert.ToInt32(reader[1].ToString()), Convert.ToInt32(reader[2].ToString()),reader[3].ToString(),reader[4].ToString(),reader[5].ToString()));
            }
            connection.Close();
            return alquileresUsuario;

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
