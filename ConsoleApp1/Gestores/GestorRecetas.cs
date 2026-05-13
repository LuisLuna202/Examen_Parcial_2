using System;
using System.Collections.Generic;
using System.Linq;
using SistemaRecetas.Interfaces;
using SistemaRecetas.Modelos;


namespace SistemaRecetas.Gestores
{
    public class GestorRecetas : IGestorRecetas
    {
        public List<Receta> RecetasDisponibles { get; set; }

        public GestorRecetas()
        {
           // Cada instancia tiene su propia lista de recetas [cite: 148]
            RecetasDisponibles = new List<Receta>();
        }

        public void AgregarReceta(Receta receta)
        {
            // Evita duplicados antes de añadir [cite: 149]
            if (!RecetasDisponibles.Any(r => r.Nombre.Equals(receta.Nombre, StringComparison.OrdinalIgnoreCase)))
            {
                RecetasDisponibles.Add(receta);
            }
        }

        public void EliminarReceta(Receta receta)
        {
           RecetasDisponibles.Remove(receta);
        }

        public void EliminarPorIndice(int indice)
        {
            if (indice >= 0 && indice < RecetasDisponibles.Count)
            {
                RecetasDisponibles.RemoveAt(indice); 
            }
        }

        public List<Receta> BuscarPorNombre(string nombre)
        {
           // Búsqueda parcial ignorando mayúsculas y minúsculas [cite: 150]
            return RecetasDisponibles
                .Where(r => r.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void LimpiarCatalogo()
        {
             RecetasDisponibles.Clear(); 
        }

        #region Algoritmos de Ordenamiento

        // QuickSort: Ordena la lista original por TiempoMinutos (Ascendente) [cite: 154, 155]
        public void QuickSort(List<Receta> lista)
        {
            if (lista == null || lista.Count <= 1) return;
            ApplyQuickSort(lista, 0, lista.Count - 1);
        }

        private void ApplyQuickSort(List<Receta> lista, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(lista, low, high);
                ApplyQuickSort(lista, low, pivotIndex - 1);
                ApplyQuickSort(lista, pivotIndex + 1, high);
            }
        }

        private int Partition(List<Receta> lista, int low, int high)
        {
            int pivot = lista[high].TiempoMinutos;
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (lista[j].TiempoMinutos <= pivot)
                {
                    i++;
                    var temp = lista[i];
                    lista[i] = lista[j];
                    lista[j] = temp;
                }
            }
            var temp2 = lista[i + 1];
            lista[i + 1] = lista[high];
            lista[high] = temp2;
            return i + 1;
        }

       // MergeSort: Retorna una nueva lista ordenada por TiempoMinutos 
        public List<Receta> MergeSort(List<Receta> lista)
        {
            if (lista.Count <= 1) return lista.ToList();

            int mid = lista.Count / 2;
            List<Receta> left = lista.GetRange(0, mid);
            List<Receta> right = lista.GetRange(mid, lista.Count - mid);

            return Merge(MergeSort(left), MergeSort(right));
        }

        private List<Receta> Merge(List<Receta> left, List<Receta> right)
        {
            List<Receta> result = new List<Receta>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if (left[i].TiempoMinutos <= right[j].TiempoMinutos)
                    result.Add(left[i++]);
                else
                    result.Add(right[j++]);
            }
            result.AddRange(left.Skip(i));
            result.AddRange(right.Skip(j));
            return result;
        }


namespace ConsoleApp1.Gestores
{
    internal class Class1
    {
    }
}