using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace VideoClub
{
    class BBDD
    {
        //CONEXION CON LA BASE DE DATOS
        static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-C1JLP92\\SQLEXPRESS;Initial Catalog=VideoClub;Integrated Security=True");
        public string Query { get; set; }

        public BBDD(){}

        //Función para hacer consultas con la base de datos
        public bool ConsultarBase()
        {
            try
            {
                SqlCommand command = new SqlCommand(Query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    connection.Close();
                    return true;
                }
                connection.Close();
                return false;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: NO SE HA PODIDO CONSULTAR LA BASE DE DATOS");
                Console.ForegroundColor = ConsoleColor.White;
                throw;
            }
            
        }

        //funcion para modificar en la base de datos
        public void ModificarBase()
        {
            try
            {
                SqlCommand command = new SqlCommand(Query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: NO SE HA PODIDO MODIFICAR LA BASE DE DATOS");
                Console.ForegroundColor = ConsoleColor.White;
                throw;
            }

        }
    }
}
