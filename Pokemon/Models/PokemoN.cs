namespace Pokemon.Models
{
    public class PokemoN
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public ICollection<Reseña> Reseñas { get; set; }
        public ICollection<EntrenadorPokemon> EntrenadorPokemon { get; set; }
        public ICollection<CategoriaPokemon> CategoriaPokemon { get; set; }
    }
}
