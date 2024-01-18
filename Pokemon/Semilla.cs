using Pokemon.Data;
using Pokemon.Models;

namespace Pokemon
{
    public class Semilla
    {
        private readonly DataContext dataContext;
        public Semilla(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.EntrenadorPokemon.Any())
            {
                var pokemonOwners = new List<EntrenadorPokemon>()
                {
                    new EntrenadorPokemon()
                    {
                        PokemoN = new PokemoN()
                        {
                            Nombre = "Pikachu",
                            FechaNacimiento = new DateTime(1903,1,1),
                            CategoriaPokemon = new List<CategoriaPokemon>()
                            {
                                new CategoriaPokemon { Categoria = new Categoria() { Nombre = "Electric"}}
                            },
                            Reseñas = new List<Reseña>()
                            {
                                new Reseña { Titulo="Pikachu",Descripcion = "Pickahu is the best pokemon, because it is electric", Rating = 5,
                                Critico = new Critico(){ PrimerNombre = "Teddy", Apellido = "Smith" } },
                                new Reseña { Titulo="Pikachu", Descripcion = "Pickachu is the best a killing rocks", Rating = 5,
                                Critico = new Critico(){ PrimerNombre = "Taylor", Apellido = "Jones" } },
                                new Reseña { Titulo="Pikachu",Descripcion = "Pickchu, pickachu, pikachu", Rating = 1,
                                Critico = new Critico(){ PrimerNombre = "Jessica", Apellido = "McGregor" } },
                            }
                        },
                        Entrenador = new Entrenador()
                        {
                            Nombre = "Jack",
                            Apellido = "London",
                            Gimnasio = "Brocks Gym",
                            Pais = new Pais()
                            {
                                Nombre = "Kanto"
                            }
                        }
                    },
                    new EntrenadorPokemon()
                    {
                        PokemoN = new PokemoN()
                        {
                            Nombre = "Squirtle",
                            FechaNacimiento = new DateTime(1903,1,1),
                            CategoriaPokemon = new List<CategoriaPokemon>()
                            {
                                new CategoriaPokemon { Categoria = new Categoria() { Nombre = "Water"}}
                            },
                            Reseñas = new List<Reseña>()
                            {
                                new Reseña { Titulo= "Squirtle", Descripcion = "squirtle is the best pokemon, because it is electric", Rating = 5,
                                Critico = new Critico(){ PrimerNombre = "Teddy", Apellido = "Smith" } },
                                new Reseña { Titulo= "Squirtle",Descripcion = "Squirtle is the best a killing rocks", Rating = 5,
                                Critico = new Critico(){ PrimerNombre = "Taylor", Apellido = "Jones" } },
                                new Reseña { Titulo= "Squirtle", Descripcion = "squirtle, squirtle, squirtle", Rating = 1,
                                Critico = new Critico(){ PrimerNombre = "Jessica", Apellido = "McGregor" } },
                            }
                        },
                        Entrenador = new Entrenador()
                        {
                            Nombre = "Harry",
                            Apellido = "Potter",
                            Gimnasio = "Mistys Gym",
                            Pais = new Pais()
                            {
                                Nombre = "Saffron City"
                            }
                        }
                    },
                    new EntrenadorPokemon()
                    {
                        PokemoN = new PokemoN()
                        {
                            Nombre = "Venasuar",
                            FechaNacimiento = new DateTime(1903,1,1),
                            CategoriaPokemon = new List<CategoriaPokemon>()
                            {
                                new CategoriaPokemon { Categoria = new Categoria() { Nombre = "Leaf"}}
                            },
                            Reseñas = new List<Reseña>()
                            {
                                new Reseña { Titulo="Veasaur",Descripcion = "Venasuar is the best pokemon, because it is electric", Rating = 5,
                                Critico = new Critico(){ PrimerNombre = "Teddy", Apellido = "Smith" } },
                                new Reseña { Titulo="Veasaur",Descripcion = "Venasuar is the best a killing rocks", Rating = 5,
                                Critico = new Critico(){ PrimerNombre = "Taylor", Apellido = "Jones" } },
                                new Reseña { Titulo="Veasaur",Descripcion = "Venasuar, Venasuar, Venasuar", Rating = 1,
                                Critico = new Critico(){ PrimerNombre = "Jessica", Apellido = "McGregor" } },
                            }
                        },
                        Entrenador = new Entrenador()
                        {
                            Nombre = "Ash",
                            Apellido = "Ketchum",
                            Gimnasio = "Ashs Gym",
                            Pais = new Pais()
                            {
                                Nombre = "Millet Town"
                            }
                        }
                    }
                };
                dataContext.EntrenadorPokemon.AddRange(pokemonOwners);
                dataContext.SaveChanges();
            }
        }
    }
}