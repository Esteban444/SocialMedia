using SocialMedia.Core.Enumerations;


namespace SocialMedia.Core.DTOs
{
    public class SecurityDTO
    {
        public string Usuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public RoleType? Rol { get; set; }
    }
}
