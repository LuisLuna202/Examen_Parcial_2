
using SistemaRecetas.Interfaces;


namespace SistemaRecetas.Modelos
{
    public class Receta : IReceta
    {
        // Propiedades
        public string Nombre { get; set; }
        public string Chef { get; set; }
        public int TiempoMinutos { get; set; }

        // Constructor
        public Receta(string nombre, string chef, int tiempoMinutos)
        {
            if (tiempoMinutos <= 0)
            {
                throw new ArgumentException(
                    "El tiempo debe ser mayor a 0 minutos."
                );
            }

            Nombre = nombre;
            Chef = chef;
            TiempoMinutos = tiempoMinutos;
        }

        // Método ToString
        public override string ToString()
        {
            return $"{Nombre} - {Chef} ({TiempoMinutos} min)";
        }
    }
}