using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace VideoClub
{
    class Usuario 
    {
        //CONEXION CON LA BASE DE DATOS
        static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-C1JLP92\\SQLEXPRESS;Initial Catalog=VideoClub;Integrated Security=True");
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }

        public Usuario(string nombre, string apellido, string fechaNacimiento, string email, string contrasena)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Email = email;
            Contrasena = contrasena;
        }
        
        public Usuario(){ } //Constructor vacio para poder Registrar usuarios preguntandole datos
        public bool Registrarse()
        {
            do
            {
                //EMAIL
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(Paso 1 de 5) Primero, necesito tu Email:");
                Console.ForegroundColor = ConsoleColor.White;
                Email = IntroducirTextoYComprobarLongitud(100);
                Email.ToLower();

                if (Email.Contains("@") && (Email.Contains(".com") || Email.Contains(".es"))) //comprobación basica para saber si es un email
                {
                    string query = $"SELECT * FROM Usuarios WHERE Email LIKE '{Email}'";

                    if (!ConsultarBase(query)) //si no existe, se puede registrar
                    {
                        //NOMBRE
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("(Paso 2 de 5) Segundo, introduce tu Nombre");
                        Console.ForegroundColor = ConsoleColor.White;
                        Nombre = IntroducirTextoYComprobarLongitud(25);

                        //APELLIDO
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("(Paso 3 de 5) Tercero, introduce tu apellido");
                        Console.ForegroundColor = ConsoleColor.White;
                        Apellido = IntroducirTextoYComprobarLongitud(25);

                        //FECHA NACIMIENTO
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("(Paso 4 de 5) Cuarto, introduce tu fecha de nacimiento (con este formato: dd/MM/aaaa)");
                        Console.ForegroundColor = ConsoleColor.White;
                        bool fechaCorrecta = false;
                        do
                        {
                            FechaNacimiento = IntroducirTextoYComprobarLongitud(10);
                            if (FechaNacimiento.Contains("/") || FechaNacimiento.Contains("-"))
                            {
                                try
                                {
                                    DateTime tempDate = Convert.ToDateTime(FechaNacimiento);
                                    if (tempDate <= DateTime.Today && tempDate.Year > (DateTime.Today.Year - 120)) //franga de control de fecha correcta
                                    {
                                        fechaCorrecta = true;
                                    }
                                    else
                                    { //ERROR POR FECHA MAL INTRODUCIDA
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("ERROR: La fecha introducida no es valida");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                catch (FormatException)
                                {
                                    //ERROR POR FECHA MAL INTRODUCIDA
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("ERROR: No has introducido un formato de fecha correcto");
                                    Console.ForegroundColor = ConsoleColor.White;

                                }
                                
                            }
                            else
                            {
                                //ERROR POR NO INTRODUCIR UNA FECHA
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR: No has introducido una fecha");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        } while (!fechaCorrecta);

                        //CONTRASEÑA
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("(Paso 5 de 5) Último, introduce tu contraseña");
                        Console.ForegroundColor = ConsoleColor.White;
                        Contrasena = IntroducirTextoYComprobarLongitud(20);

                        //COMPROBACIÓN DE DATOS
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Por favor, comprueba que estos datos son correctos (Si/No)");
                        Console.ForegroundColor = ConsoleColor.White;

                        MostrarDatos();
                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Itroduce Si o No");
                            Console.ForegroundColor = ConsoleColor.White;
                            string opcion = Console.ReadLine().ToUpper();

                            if (opcion == "SI")
                            {
                                //introducirlos en la base de datos
                                DateTime tempDate = Convert.ToDateTime(FechaNacimiento);
                                tempDate.ToString("MM/dd/yyyy");
                                query = $"INSERT INTO Usuarios(Nombre, Apellido, FechaNacimiento, Email, Contrasena) VALUES ('{Nombre}','{Apellido}','{tempDate.Month}/{tempDate.Day}/{tempDate.Year}','{Email}','{Contrasena}')";
                                ModificarBase(query);
                                return true;
                            }
                            else if (opcion == "NO")
                            {
                                //Preguntar si quiere modificar o salir
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("¿Quieres modificarlos o quieres salir? ");
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("(Pulsa cualquier tecla para Modificar / introduce S para salir)");
                                Console.ForegroundColor = ConsoleColor.White;
                                opcion = Console.ReadLine().ToUpper();
                                if (opcion == "S")
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR: No has introducido una opcion valida");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        } while (true);
                        
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("El email introducido ya existe, entra con tu email y contraseña o utiliza otro email para registrarte");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: No has introducido un email valido");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (true);

        }
        public string IntroducirTextoYComprobarLongitud(int longitud)
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Itroduce el dato solicitado");
                Console.ForegroundColor = ConsoleColor.White;
                string texto = Console.ReadLine();
                if (texto.Length <= longitud)
                {
                    return texto;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Has introducido un valor demasiado largo, por favor, vuelve a intentarlo");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (true);
        }
        public bool Loguearse()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Introduce tu Email");
            Console.ForegroundColor = ConsoleColor.White;
            Email = IntroducirTextoYComprobarLongitud(100);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Introduce tu Contraseña");
            Console.BackgroundColor = ConsoleColor.White;
            Contrasena = IntroducirTextoYComprobarLongitud(25);
            Console.BackgroundColor = ConsoleColor.Black;
            string query = $"SELECT * FROM Usuarios WHERE Email Like '{Email}' AND Contrasena Like '{Contrasena}'";
            if (ConsultarBase(query)) 
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Id = Convert.ToInt32(reader[0].ToString());
                        Nombre = reader[1].ToString();
                        Apellido = reader[2].ToString();
                        FechaNacimiento = reader[3].ToString();
                        Email = reader[4].ToString();
                        Contrasena = reader[5].ToString();
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
                
            }
            return false;
        }
        public void MostrarDatos()
        {
            Console.WriteLine("- - - - - - - - - - -");
            Console.WriteLine("- Datos Usuario -");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Apellido: {Apellido}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Fecha Nacimiento: {FechaNacimiento}");
            Console.WriteLine($"Contraseña: {Contrasena}");
            Console.WriteLine("- - - - - - - - - - -");
        }
        //Función para hacer consultas con la base de datos
        public bool ConsultarBase(string query)
        {
            try
            {
                
                SqlCommand command = new SqlCommand(query, connection);
                connection.Close();
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
