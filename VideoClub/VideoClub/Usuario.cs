using System;
using System.Collections.Generic;
using System.Text;

namespace VideoClub
{
    class Usuario
    {
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
        public void Registrarse()
        {
            Console.WriteLine("Introduce tu email");
            Email = Console.ReadLine();
            if (Email.Contains("@") && (Email.Contains(".com")|| Email.Contains(".es")))
            {
                //MIRAR SI YA EXISTE ESE EMAIL O NO
            }
        }
    }
}
