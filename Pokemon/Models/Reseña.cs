namespace Pokemon.Models
{
    public class Reseña
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Rating { get; set; }
        public Critico Critico { get; set; } //1 a 1 relacion
        public PokemoN PokemoN { get; set; }

    }
}
