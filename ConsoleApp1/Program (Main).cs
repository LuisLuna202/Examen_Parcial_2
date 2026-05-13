using System;
using System.Collections.Generic;
using SistemaRecetas.Gestores;
using SistemaRecetas.Modelos;
using SistemaRecetas.Servicios;

namespace SistemaRecetas
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.- Inicializa
            GestorRecetas gestor = new GestorRecetas();
            ExportadorTxt exportador = new ExportadorTxt();
            ServicioRecetas servicio = new ServicioRecetas(gestor, exportador);

            // 2. Añadir 8 recetas de ejemplo 
            gestor.AgregarReceta(new Receta("Paella", "Chef Ramírez", 45));
            gestor.AgregarReceta(new Receta("Tacos", "Chef García", 30));
            gestor.AgregarReceta(new Receta("Risotto", "Chef Rossi", 50));
            gestor.AgregarReceta(new Receta("Ceviche", "Chef López", 20));
            gestor.AgregarReceta(new Receta("Ramen", "Chef Tanaka", 90));
            gestor.AgregarReceta(new Receta("Guacamole", "Chef Méndez", 10));
            gestor.AgregarReceta(new Receta("Croissant", "Chef Pierre", 120));
            gestor.AgregarReceta(new Receta("Tiramisu", "Chef Bianchi", 40));

            // 3. Registro de Usuario Inicial 
            Console.WriteLine("SISTEMA DE GESTIÓN DE RECETAS DE COCINA");
            Console.WriteLine("REGISTRO DE USUARIO");
            Console.Write("Por favor, ingrese su nombre de usuario: ");
            string nombreUsuario = Console.ReadLine();
            Usuario usuarioActual = servicio.RegistrarUsuario(nombreUsuario);

            Console.Write("Ingrese un nombre para su primer libro de recetas: ");
            string nombreLibro = Console.ReadLine();
            usuarioActual.CrearLibroRecetas(nombreLibro);
            string libroActual = nombreLibro;

            bool salir = false;

            // 4. Menú Principal (8 opciones) 
            while (!salir)
            {
                Console.WriteLine($"\n--- MENÚ PRINCIPAL ---");
                Console.WriteLine($"Libro actual: '{libroActual}' ({usuarioActual.LibrosRecetas[libroActual].Count} recetas) [cite: 305]");
                Console.WriteLine("1. Mostrar recetas disponibles");
                Console.WriteLine("2. Ordenar libro actual (QuickSort o MergeSort)");
                Console.WriteLine("3. Búsqueda binaria en catálogo");
                Console.WriteLine("4. Crear nuevo libro de recetas");
                Console.WriteLine("5. Cambiar de libro actual");
                Console.WriteLine("6. Ver mis libros");
                Console.WriteLine("7. Exportar mis libros a archivo .txt");
                Console.WriteLine("8. Salir");
                Console.Write("Seleccione una opción: ");

                 if (!int.TryParse(Console.ReadLine(), out int opcion)) 
               
                switch (opcion)
                {
                    case 1: // Mostrar recetas 
                    Console.WriteLine("\nRecetas en el catálogo:");
                    var catalogo = gestor.RecetasDisponibles;
                    for (int i = 0; i < catalogo.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {catalogo[i]}");
                    }
                    Console.Write("Ingrese el número de la receta para añadirla a su libro (o 0 para cancelar): ");
                    if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= catalogo.Count)
                    {
                        usuarioActual.AgregarRecetaALibro(libroActual, catalogo[idx - 1]);
                        Console.WriteLine("Receta añadida.");
                    }
                    break;

                    case 2: // Ordenar libro y calcular tiempo 
                        Console.WriteLine("Seleccione algoritmo: 1. QuickSort, 2. MergeSort");
                        string alg = Console.ReadLine() == "1" ? "quick" : "merge";

                        // Ordenamos el catálogo global como ejemplo de uso de los algoritmos
                        servicio.OrdenarCatalogo(alg); 
                        
                        int tiempoTotal = servicio.OrdenarLibroYCalcularTiempo(usuarioActual, libroActual); 
                        Console.WriteLine($"Tiempo total de preparación en '{libroActual}': {tiempoTotal} min.");
                        break;

                        case 3: // Búsqueda binaria 
                            Console.Write("Nombre de la receta a buscar: ");
                            string busca = Console.ReadLine();
                            int resultadoIdx = gestor.BusquedaBinaria(busca);

                            if (resultadoIdx != -1)
                            {
                                Receta encontrada = gestor.RecetasDisponibles[resultadoIdx];
                                Console.WriteLine($"¡Receta encontrada!: {encontrada}");
                                Console.Write("¿Desea añadirla al libro actual? (S/N): ");
                                if (Console.ReadLine().ToUpper() == "S")
                                {
                                    usuarioActual.AgregarRecetaALibro(libroActual, encontrada);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No se encontró la receta exacta.");
                            }
                            break;

                            case 4: // Crear nuevo libro 
                                Console.Write("Nombre del nuevo libro: ");
                                string nuevoLibro = Console.ReadLine();
                                try
                                {
                                    usuarioActual.CrearLibroRecetas(nuevoLibro);
                                    Console.WriteLine("Libro creado.");
                                }
                        catch (InvalidOperationException ex)
                        {
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                                break;

                                case 5: // Cambiar de libro 
                                    Console.WriteLine("Libros disponibles:");
                                    foreach (var l in usuarioActual.LibrosRecetas.Keys) Console.WriteLine($"- {l}");
                                    Console.Write("Ingrese el nombre del libro al que desea cambiar: ");
                                    string cambio = Console.ReadLine();
                                    if (usuarioActual.LibrosRecetas.ContainsKey(cambio))
                                    {
                                        libroActual = cambio;
                                        Console.WriteLine($"Cambiado a: {libroActual}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("El libro no existe.");
                                    }
                                    break;

                                    case 6: // Ver mis libros 
                                         usuarioActual.MostrarLibros(); 
                        break;

                                        case 7: // Exportar a .txt 
                                            string ruta = $"{usuarioActual.Nombre}_Recetas.txt";
                                             servicio.Exportador.ExportarATxt(usuarioActual, ruta); 
                        break;

                                            case 8: // Salir 
                                                salir = true;
                                                break;
                }
            }
        }
    }
                                
}
                        

