using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infraestructure.Interfaces
{
    //agregada para encriptacion de contraseña
    public interface IPasswordService 
    {
        string Hash(string password);

        bool Check(string hash, string password);
    }
}
