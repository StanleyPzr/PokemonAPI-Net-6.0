namespace Pokemon.Models
{
    public class Pais
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Entrenador> Entrenadores { get; set; }
    }
}
