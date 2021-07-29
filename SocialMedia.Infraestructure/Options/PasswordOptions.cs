using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infraestructure.Options
{
    public class PasswordOptions
    {
        // Agregada para la encriptacion de la contraseña
        public int SaltSize { get; set; }
        public int KeySize { get; set; }
        public int Iterations { get; set; } 

    }
}
