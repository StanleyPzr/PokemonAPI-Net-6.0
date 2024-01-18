namespace Pokemon.Models
{
    public class Entrenador
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Gimnasio{ get; set; }
        public Pais Pais { get; set; } //1 a 1 relacion
        public ICollection<EntrenadorPokemon> EntrenadorPokemon { get; set; } // 1 a muchos relacion

    }
}
