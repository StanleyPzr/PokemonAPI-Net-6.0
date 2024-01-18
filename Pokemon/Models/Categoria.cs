namespace Pokemon.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<CategoriaPokemon> CategoriaPokemon {  get; set; }
    }
}
