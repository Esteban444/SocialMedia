using SocialMedia.Core.Enumerations;


namespace SocialMedia.Core.Entities
{
    public class Security: BaseEntity
    {

        public string Usuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public RoleType Rol { get; set; } 
    }
}
