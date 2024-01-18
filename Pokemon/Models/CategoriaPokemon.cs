namespace Pokemon.Models
{
    public class CategoriaPokemon
    {
        public int IdPokemon{ get; set; }
        public int IdCategoria { get; set; }
        public PokemoN PokemoN { get; set; }
        public Categoria Categoria { get; set; }    
    }
}
