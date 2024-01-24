using System.Text.Json.Serialization;

namespace Pokemon.Models.Auth
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        [JsonIgnore] public string Contraseña { get; set; }
    }
}
