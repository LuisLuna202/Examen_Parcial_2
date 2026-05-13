using System;
using System.IO;
using SistemaRecetas.Interfaces;
using SistemaRecetas.Modelos;

namespace SistemaRecetas.Servicios
{
    public class ExportadorTxt : IExportador
    {
        public void ExportarATxt(Usuario usuario, string rutaArchivo)
        {
            try
            {
                // Se utiliza StreamWriter para crear y escribir en el archivo 
                using (StreamWriter writer = new StreamWriter(rutaArchivo))
                {
                    // 1. Escribir encabezado con nombre y fecha actual 
                    writer.WriteLine("============================================");
                    writer.WriteLine("      REPORTE DE LIBROS DE RECETAS");
                    writer.WriteLine("============================================");
                    writer.WriteLine($"Usuario: {usuario.Nombre}");
                    writer.WriteLine($"Fecha de exportación: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    writer.WriteLine("============================================\n");

                    // 2. Seguido de la información de cada receta en los Libros Recetas 
                    if (usuario.LibrosRecetas.Count == 0)
                    {
                        writer.WriteLine("El usuario no tiene libros registrados.");
                    }
                    else
                    {
                        foreach (var libro in usuario.LibrosRecetas)
                        {
                            writer.WriteLine($"LIBRO: {libro.Key.ToUpper()}");
                            writer.WriteLine(new string('-', 30));

                            if (libro.Value.Count == 0)
                            {
                                writer.WriteLine("   (Este libro no tiene recetas)");
                            }
                            else
                            {
                                foreach (var receta in libro.Value)
                                {
                                    // Se utiliza el formato ToString() definido en la clase Receta 
                                    writer.WriteLine($"   [ ] {receta.ToString()}");
                                }
                            }
                            writer.WriteLine(); // Espacio entre libros
                        }
                    }

                    writer.WriteLine("============================================");
                    writer.WriteLine("             Fin del Reporte");
                }

                Console.WriteLine($"\nSistema: Archivo exportado exitosamente en: {rutaArchivo}");
            }
            catch (Exception ex)
            {
                // Manejo básico de errores de E/S
                Console.WriteLine($"Error al exportar el archivo: {ex.Message}");
            }
        }
    }
}
