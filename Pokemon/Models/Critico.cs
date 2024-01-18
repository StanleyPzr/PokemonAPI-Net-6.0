namespace Pokemon.Models
{
    public class Critico
    {
        public int Id { get; set; }
        public string PrimerNombre { get; set; }
        public string Apellido { get; set; }
        public ICollection<Reseña> Reseñas { get; set; }
    }
}
