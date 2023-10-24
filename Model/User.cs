using Microsoft.AspNetCore.Mvc;

namespace tTrackerWeb.Model
{
    public class User
    {
        public int Id { get; set; } // Identificador único del usuario
        public string Username { get; set; } // Nombre de usuario
        public string Password { get; set; } // Contraseña del usuario (asegúrate de almacenarla de forma segura)
        public UserRole Role { get; set; } // Rol del usuario (Administrador, Usuario Responsable, Usuario Normal)

    }
}
