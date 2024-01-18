namespace Pokemon.Models
{
    public class EntrenadorPokemon
    {
        public int IdPokemon { get; set; }
        public int IdEntrenador { get; set; }
        public PokemoN PokemoN { get; set; }
        public Entrenador Entrenador { get; set; }
    }
}
