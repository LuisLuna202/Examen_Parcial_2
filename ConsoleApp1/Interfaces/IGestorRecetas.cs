using SistemaRecetas.Interfaces;
using SistemaRecetas.Modelos;

namespace SistemaRecetas.Interfaces
{
    public interface IGestorRecetas
    {
        
        List<Receta> RecetasDisponibles { get; set; }

        void AgregarReceta(Receta receta);
        void EliminarReceta(Receta receta);
        void EliminarPorIndice(int indice);
        List<Receta> BuscarPorNombre(string nombre);
        void LimpiarCatalogo();

        
        void QuickSort(List<Receta> lista);
        List<Receta> MergeSort(List<Receta> lista);
        int BusquedaBinaria(string nombre);
    }
}