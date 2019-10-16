using System;

namespace VideoClub
{
    class Program
    {
        static Usuario usuario = new Usuario();
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
                Console.WriteLine("\n.-_-_-_ VIDEOCLUB REINA _-_-_-.\nBienvenido, ¿Qué quieres hacer?\n-------------------------------\n\t1.Registrarme\n\t2.Entrar\n\t3.Salir");
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
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n.-_-_-_ VIDEOCLUB REINA _-_-_-.\nBienvenido {usuario.Nombre}, ¿Qué quieres hacer?\n-------------------------------\n\t1. Ver cartelera de Peliculas\n\t2. Alquilar Pelicula \n\t3.Mis Alquileres \n\t4.Salir");
                //AQUIIIIIIIIIIIIIIIIII
                bool opcionValida = false;
                do
                {
                    int opcion = IntroducirOpcion();
                    switch (opcion)
                    {
                        case 1:

                            break;
                        case 2:

                            break;

                        case 3:
                            break;

                        case 4:

                            break;
                        default:

                            break;
                    }
                } while (!opcionValida);
            } while (true);
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
    }
}
