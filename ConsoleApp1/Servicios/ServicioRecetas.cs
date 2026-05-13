using SistemaRecetas.Modelos;
using SistemaRecetas.Servicios;
using SistemaRecetas.Interfaces;
using SistemaRecetas.Gestores;

namespace SistemaRecetas.Servicios
{
    public class ServicioRecetas
    {
        // Propiedades del sistema 
        public IGestorRecetas Gestor { get; set; } // Acceso al catálogo 
        public IExportador Exportador { get; set; } // Servicio de exportación 
        public List<Usuario> Usuarios { get; set; } // Usuarios registrados 

        // Constructor con inyección de dependencias 
        public ServicioRecetas(IGestorRecetas gestor, IExportador exportador)
        {
            Gestor = gestor;
            Exportador = exportador;
            Usuarios = new List<Usuario>();
        }

        // Crea un usuario, lo añade a la lista y lo retorna 
        public Usuario RegistrarUsuario(string nombre)
        {
            Usuario nuevoUsuario = new Usuario(nombre);
            Usuarios.Add(nuevoUsuario);
            return nuevoUsuario;
        }

        // Retorna el Usuario buscando por nombre ignorando mayúsculas/minúsculas 
        public Usuario BuscarUsuario(string nombre)
        {
            return Usuarios.FirstOrDefault(u => u.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        // Elimina un usuario y retorna si tuvo éxito 
        public bool EliminarUsuario(string nombre)
        {
            Usuario usuario = BuscarUsuario(nombre);
            if (usuario != null)
            {
                return Usuarios.Remove(usuario);
            }
            return false;
        }

        // Retorna el número de usuarios registrados 
        public int ContarUsuarios()
        {
            return Usuarios.Count;
        }

        // Ordena el catálogo global según el algoritmo especificado 
        public void OrdenarCatalogo(string tipo)
        {
            if (tipo.ToLower() == "quick")
            {
                Gestor.QuickSort(Gestor.RecetasDisponibles);
                Console.WriteLine("Sistema: Catálogo ordenado exitosamente usando QuickSort."); 
            }
            else if (tipo.ToLower() == "merge")
            {
                Gestor.RecetasDisponibles = Gestor.MergeSort(Gestor.RecetasDisponibles);
                Console.WriteLine("Sistema: Catálogo ordenado exitosamente usando MergeSort."); 
            }
            else
            {
                Console.WriteLine("Algoritmo no válido. Use 'quick' o 'merge'.");
            }
        }

      // Retorna la suma total de TiempoMinutos de las recetas en un libro específico 
        public int OrdenarLibroYCalcularTiempo(Usuario usuario, string nombreLibro)
        {
            var libro = usuario.ObtenerLibro(nombreLibro);

            if (libro == null)
            {
                return 0;
            }

            
            return libro.Sum(r => r.TiempoMinutos);
        }
    }
}

