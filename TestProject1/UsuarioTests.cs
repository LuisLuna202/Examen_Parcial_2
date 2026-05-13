using Xunit;
using SistemaRecetas.Modelos;

namespace SistemaRecetas.Test
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public Dictionary<string, List<Receta>> LibrosRecetas { get; set; }


        public Usuario(string nombre)
        {
            Nombre = nombre;
            LibrosRecetas = new Dictionary<string, List<Receta>>();
        }

        public void CrearLibroRecetas(string nombreLibro)
        {

            if (LibrosRecetas.ContainsKey(nombreLibro))
            {
                throw new InvalidOperationException($"El libro '{nombreLibro}' ya existe.");
            }
            LibrosRecetas.Add(nombreLibro, new List<Receta>());
        }

        public void AgregarRecetaALibro(string nombreLibro, Receta receta)
        {

            if (!LibrosRecetas.ContainsKey(nombreLibro))
            {
                throw new KeyNotFoundException($"No se encontró el libro '{nombreLibro}'.");
            }
            LibrosRecetas[nombreLibro].Add(receta);
        }

        public List<Receta> ObtenerLibro(string nombreLibro)
        {
            return LibrosRecetas.ContainsKey(nombreLibro) ? LibrosRecetas[nombreLibro] : null;
        }

        public int ContarRecetas()
        {
            return LibrosRecetas.Values.Sum(lista => lista.Count);
        }

        public void MostrarLibros()
        {
            if (LibrosRecetas.Count == 0) return;

            foreach (var libro in LibrosRecetas)
            {
                Console.WriteLine($"Libro: {libro.Key}");
                foreach (var receta in libro.Value)
                {
                    Console.WriteLine($"  - {receta.ToString()}");
                }
            }
        }
    }
}