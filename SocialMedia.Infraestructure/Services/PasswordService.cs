using Microsoft.Extensions.Options;
using SocialMedia.Infraestructure.Interfaces;
using SocialMedia.Infraestructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SocialMedia.Infraestructure.Services
{
    //agregado para sifrado de contraseña y validacion para comparar con uno ya guardado
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions _Options;
        public PasswordService(IOptions<PasswordOptions> options)
        {
            _Options = options.Value;
        }

        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if(parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
                   password,
                   salt,
                   iterations
                ))
            {
                var keyToCheck = algorithm.GetBytes(_Options.KeySize);
                return keyToCheck.SequenceEqual(key);
            }
        }

        public string Hash(string password)
        {
            //PBKDF2 implementation
            using (var algorithm = new Rfc2898DeriveBytes(
                   password,
                   _Options.SaltSize,
                   _Options.Iterations
                ))
            {
                var key = Convert.ToBase64String( algorithm.GetBytes(_Options.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);
                return $"{_Options.Iterations}.{salt}.{key}";
            }
        }
    }
}
