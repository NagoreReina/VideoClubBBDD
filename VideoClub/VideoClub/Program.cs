using System;
using System.Collections.Generic;

namespace VideoClub
{
    class Program
    {
        static Usuario usuario = new Usuario();
        static Pelicula pelicula = new Pelicula();
        static Alquiler alquiler = new Alquiler();
        static List<Pelicula> peliculas = pelicula.AlmacenarPeliculas();
        static void Main(string[] args)
        {
            MenuInicial();
        }
        public static void MenuInicial()
        {
            bool finalizarPrograma = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n.-_-_-_ VIDEOCLUB REINA _-_-_-.\nBienvenid@, ¿Qué quieres hacer?\n-------------------------------\n\t1.Registrarme\n\t2.Entrar\n\t3.Salir");
                Console.ForegroundColor = ConsoleColor.White;
                bool opcionValida = false;
                do
                {
                    int opcion = IntroducirOpcion();
                    switch (opcion)
                    {
                        case 1:
                            //registro
                            if (usuario.Registrarse())
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Registro completado correctamente");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            opcionValida = true;
                            break;
                        case 2:
                            //loggin
                            usuario = new Usuario();
                            bool salirDelLoggin = false;
                            do
                            {
                                if (usuario.Loguearse())
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Te has conectado correctamente");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    MenuUsuario();
                                    opcionValida = true;
                                    salirDelLoggin = true;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("ERROR: Los datos introducidos no son correctos");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("¿Quieres volver a intentarlo? (Si/No)");
                                    bool terminarBucle = false;
                                    do
                                    {
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine("Itroduce Si o No");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        string seleccion = Console.ReadLine().ToUpper();

                                        if (seleccion == "SI")
                                        {
                                            terminarBucle = true;
                                        }
                                        else if (seleccion == "NO")
                                        {
                                            terminarBucle = true;
                                            salirDelLoggin = true;
                                            opcionValida = true;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("ERROR: No has introducido una opcion valida");
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                    } while (!terminarBucle);

                                }
                            } while (!salirDelLoggin);
                            break;
                        case 3:
                            //salir
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Gracias por tu visita, esperamos verte pronto");
                            Console.ForegroundColor = ConsoleColor.White;
                            opcionValida = true;
                            finalizarPrograma = true;
                            break;
                        default:
                            //opcion no valida
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR: Esa opción no es valida");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                } while (!opcionValida);
            } while (!finalizarPrograma);
            
            
        }
        public static void MenuUsuario()
        {
            bool salirDelMenu = false;
            List<Pelicula> peliculasRecomendadas = GenerarPeliculasRecomendadas();
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n.-_-_-_ VIDEOCLUB REINA _-_-_-.\nBienvenid@ {usuario.Nombre}, ¿Qué quieres hacer?\n-------------------------------\n\t1. Ver cartelera de Peliculas\n\t2. Alquilar Pelicula \n\t3.Mis Alquileres \n\t4.Salir");
                int opcion = IntroducirOpcion();
                switch (opcion)
                {
                    case 1:
                        //ACCEDER A LAS PELICULAS DISPONIBLES
                        Console.WriteLine("--------------------------------------------");
                        foreach (Pelicula pelicula in peliculasRecomendadas)
                        {
                            Console.WriteLine($"{pelicula.Id} - {pelicula.Nombre} \nDisponible: {pelicula.Disponible}");
                            Console.WriteLine("--------------------------------------------");
                            
                        }
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("¿Quieres mas información de alguna? (introduce el numero de la pelicula de la que quieres más información, o -1 para salir)");
                        int seleccion = IntroducirOpcion();
                        if (seleccion > 0)
                        {
                            bool peliculaCorrecta = true;
                            for (int i = 0; i <= peliculasRecomendadas.Count; i++)
                            {
                                if (i == peliculasRecomendadas.Count)
                                {
                                    peliculaCorrecta = false;
                                }
                                else
                                {
                                    if (seleccion == peliculasRecomendadas[i].Id)
                                    {
                                        Console.WriteLine("--------------------------------------------");
                                        Console.WriteLine($"{peliculasRecomendadas[i].Id} - {peliculasRecomendadas[i].Nombre} \nDisponible: {peliculasRecomendadas[i].Disponible} \nDescripción: \n{peliculasRecomendadas[i].Descripcion}");
                                        Console.WriteLine("--------------------------------------------");
                                        i = peliculasRecomendadas.Count;
                                    }
                                }
                            }
                            if (!peliculaCorrecta)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR: Esa pelicula no existe");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        break;
                    case 2:
                        //CREAR UN NUEVO ALQUILER
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("¿Qué pelicula quieres alquilar? (Introduce el nº de la pelicula que quieres alquilar)");
                        Console.ForegroundColor = ConsoleColor.White;
                        seleccion = IntroducirOpcion();
                        bool alquilerRealizado = false;
                        bool noExistePelicula = false;
                        for (int i = 0; i <= peliculasRecomendadas.Count; i++)
                        {
                            if (i == peliculasRecomendadas.Count)
                            {
                                noExistePelicula = true;
                            }
                            else
                            {
                                if (seleccion == peliculasRecomendadas[i].Id && peliculasRecomendadas[i].Disponible == "SI" && peliculasRecomendadas[i].Stock > 0)
                                {
                                    alquilerRealizado = alquiler.GenerarAlquiler(usuario.Id, peliculasRecomendadas[i].Id);
                                    if (peliculasRecomendadas[i].Stock -1 <= 0)
                                    {
                                        peliculasRecomendadas[i].ModificarBase($"UPDATE Peliculas SET Disponible = 'NO', Stock = {peliculasRecomendadas[i].Stock - 1} WHERE Id = {peliculasRecomendadas[i].Id}");
                                    }
                                    else
                                    {
                                        peliculasRecomendadas[i].ModificarBase($"UPDATE Peliculas SET Stock = {peliculasRecomendadas[i].Stock - 1} WHERE Id = {peliculasRecomendadas[i].Id}");
                                    }
                                    
                                    peliculas = pelicula.AlmacenarPeliculas();
                                    peliculasRecomendadas = GenerarPeliculasRecomendadas();

                                    i = peliculasRecomendadas.Count;
                                }
                            }
                        }
                        if (noExistePelicula)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR: Esa pelicula no existe o no esta disponible");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            if (alquilerRealizado)
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("La pelicula está alquilada durante 2 dias, puedes ver la información en Mis Alquileres, Gracias");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        
                        break;

                    case 3:
                        //ACCEDER A LOS ALQUILERES DEL USUARIO
                        bool salirDeMisAlquileres = false;
                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;

                            Console.WriteLine($"\n.______Mis Alquileres______.\n");
                            List<Alquiler> alquileresUsuario = alquiler.AlquileresDeUsuario(usuario.Id);
                            if (alquileresUsuario.Count > 0)
                            {
                                foreach (Alquiler alquiler in alquileresUsuario)
                                {
                                    string nombrePelicula = "";
                                    if (alquiler.Devuelta == "NO")
                                    {
                                        foreach (Pelicula pelicula in peliculas)
                                        {
                                            if (pelicula.Id == alquiler.IdPelicula)
                                            {
                                                nombrePelicula = pelicula.Nombre;
                                            }
                                        }
                                        DateTime fechaEntrega = Convert.ToDateTime(alquiler.FechaAlquiler).AddDays(2);
                                        if (fechaEntrega < DateTime.Today)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"Nº {alquiler.Id} Nombre: {nombrePelicula} Fecha Alquiler: {Convert.ToDateTime(alquiler.FechaAlquiler).ToString("dd/MM/yyyy")} Fecha Limite: {Convert.ToDateTime(fechaEntrega).ToString("dd/MM/yyyy")}");
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Nº {alquiler.Id} Nombre: {nombrePelicula} Fecha Alquiler: {Convert.ToDateTime(alquiler.FechaAlquiler).ToString("dd/MM/yyyy")} Fecha Limite: {Convert.ToDateTime(fechaEntrega).ToString("dd/MM/yyyy")}");
                                        }
                                        
                                    }

                                }
                            }
                            Console.WriteLine($"\n¿Qué quieres hacer, {usuario.Nombre}?\n-------------------------------\n\t1. Devolver una pelicula\n\t2. Salir");
                            opcion = IntroducirOpcion();
                            if (alquileresUsuario.Count == 0 && opcion == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR: No tienes peliculas alquiladas");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                switch (opcion)
                                {
                                    case 1:
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine($"¿Qué pelicula quieres devolver? (introduce su número)");
                                        opcion = IntroducirOpcion();
                                        bool seEncontroAlquiler = false;
                                        for (int i = 0; i < alquileresUsuario.Count; i++)
                                        {
                                            if (alquileresUsuario[i].Id == opcion)
                                            {
                                                seEncontroAlquiler = true;
                                                Pelicula peliculaADevolver = new Pelicula();
                                                //SUMAR EL STOCK------------------------------------------------------------------------------------------------------------
                                                foreach (Pelicula pelicula in peliculas)
                                                {
                                                    if (alquileresUsuario[i].IdPelicula == pelicula.Id)
                                                    {
                                                        peliculaADevolver = pelicula;
                                                    }
                                                }
                                                pelicula.ModificarBase($"UPDATE Peliculas Set Disponible = 'SI', Stock = {peliculaADevolver.Stock +1} WHERE Id = {alquileresUsuario[i].IdPelicula}");
                                                i = alquileresUsuario.Count;
                                            }
                                        }
                                        if (seEncontroAlquiler)
                                        {
                                            //DEVOLVER
                                            alquiler.DevolverPelicula(opcion);
                                            Console.ForegroundColor = ConsoleColor.Magenta;
                                            Console.WriteLine("La pelicula está devuelta, esperamos que la hayas disfrutado, Gracias");
                                            Console.ForegroundColor = ConsoleColor.White;
                                            peliculas = pelicula.AlmacenarPeliculas();
                                            peliculasRecomendadas = GenerarPeliculasRecomendadas();
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("ERROR: Esa pelicula no está entre tus alquileres");
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }

                                        break;
                                        case 2:
                                        salirDeMisAlquileres = true;
                                        break;
                                    default:
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("ERROR: Esa opción no existe");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        break;
                                }
                            }
                        } while (!salirDeMisAlquileres);
                        break;

                    case 4:
                        salirDelMenu = true;
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"Gracias por tu visita {usuario.Nombre}, esperamos verte pronto");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: Esa opción no existe");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            } while (!salirDelMenu);

        }
        public static int IntroducirOpcion() //funcion que comprueba se repite hasta que el usuario introduce un numero
        {
            int opcion = 0;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Introduce tu selección (solo se aceptan números)");
                Console.ForegroundColor = ConsoleColor.White;
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    return opcion;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: No has introducido un número");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                
            } while (true);

        }
        public static List<Pelicula> GenerarPeliculasRecomendadas()
        {
            List<Pelicula> peliculasRecomendadas = new List<Pelicula>();
            TimeSpan ts = (DateTime.Today - Convert.ToDateTime(usuario.FechaNacimiento));
            DateTime zeroTime = new DateTime(1, 1, 1);
            int edad = (zeroTime + ts).Year - 1;
            for (int i = 0; i < peliculas.Count; i++)
            {
                if (peliculas[i].EdadRecomendada <= edad)
                {
                    peliculasRecomendadas.Add(peliculas[i]);
                }
            }
            return peliculasRecomendadas;
        }
    }
}
