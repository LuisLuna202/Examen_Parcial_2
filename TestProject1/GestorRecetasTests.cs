

using Xunit;
using SistemaRecetas.Gestores;

namespace SistemaRecetas.Test
{
    public class GestorRecetasTests
    {
        // 1. AgregarReceta / EliminarReceta
        [Fact]
        public void AgregarReceta_DebeAumentarCount()
        {
            // Arrange
            GestorRecetas g = new GestorRecetas();

            Receta receta = new Receta(
                "Paella Valenciana",
                "Chef Ramírez",
                45
            );

            // Act
            g.AgregarReceta(receta);

            // Assert
            Assert.Equal(
                1,
                g.RecetasDisponibles.Count
            );
        }

        [Fact]
        public void EliminarReceta_DebeDisminuirCount()
        {
            // Arrange
            GestorRecetas g = new GestorRecetas();

            Receta receta = new Receta(
                "Paella Valenciana",
                "Chef Ramírez",
                45
            );

            g.AgregarReceta(receta);

            // Act
            g.EliminarReceta(receta);

            // Assert
            Assert.Equal(
                0,
                g.RecetasDisponibles.Count
            );
        }

        [Fact]
        public void AgregarReceta_Duplicada_NoDebeAgregar()
        {
            // Arrange
            GestorRecetas g = new GestorRecetas();

            Receta r1 = new Receta(
                "Paella Valenciana",
                "Chef Ramírez",
                45
            );

            Receta r2 = new Receta(
                "PAELLA VALENCIANA",
                "Otro Chef",
                50
            );

            // Act
            g.AgregarReceta(r1);
            g.AgregarReceta(r2);

            // Assert
            Assert.Single(
                g.RecetasDisponibles
            );
        }

        // 2. BuscarPorNombre()
        [Fact]
        public void BuscarPorNombre_DebeEncontrarCoincidenciasParciales()
        {
            // Arrange
            GestorRecetas g = new GestorRecetas();

            g.AgregarReceta(new Receta(
                "Paella Valenciana",
                "Chef Ramírez",
                45
            ));

            g.AgregarReceta(new Receta(
                "Tacos",
                "Chef Luis",
                20
            ));

            // Act
            List<Receta> resultados =
                g.BuscarPorNombre("paella");

            // Assert
            Assert.Contains(
                resultados,
                r => r.Nombre == "Paella Valenciana"
            );
        }

        [Fact]
        public void BuscarPorNombre_SinCoincidencias_DebeRetornarVacio()
        {
            // Arrange
            GestorRecetas g = new GestorRecetas();

            g.AgregarReceta(new Receta(
                "Paella",
                "Chef Ramírez",
                45
            ));

            // Act
            List<Receta> resultados =
                g.BuscarPorNombre("Pizza");

            // Assert
            Assert.Empty(resultados);
        }

        // 3. QuickSort()
        [Fact]
        public void QuickSort_DebeOrdenarPorTiempoAscendente()
        {
            // Arrange
            GestorRecetas g = new GestorRecetas();

            List<Receta> lista = new List<Receta>()
            {
                new Receta("A", "Chef", 50),
                new Receta("B", "Chef", 20),
                new Receta("C", "Chef", 35)
            };

            // Act
            g.QuickSort(lista);

            // Assert
            for (int i = 0; i < lista.Count - 1; i++)
            {
                Assert.True(
                    lista[i].TiempoMinutos
                    <= lista[i + 1].TiempoMinutos
                );
            }
        }

        // MergeSort()
        [Fact]
        public void MergeSort_DebeOrdenarSinModificarOriginal()
        {
            // Arrange
            GestorRecetas g = new GestorRecetas();

            List<Receta> original = new List<Receta>()
            {
                new Receta("A", "Chef", 50),
                new Receta("B", "Chef", 20),
                new Receta("C", "Chef", 35)
            };

            // Act
            List<Receta> ordenada =
                g.MergeSort(original);

            // Assert orden
            for (int i = 0; i < ordenada.Count - 1; i++)
            {
                Assert.True(
                    ordenada[i].TiempoMinutos
                    <= ordenada[i + 1].TiempoMinutos
                );
            }

            // Assert original intacta
            Assert.Equal(
                50,
                original[0].TiempoMinutos
            );
        }

        // 4. BusquedaBinaria()
        [Fact]
        public void BusquedaBinaria_RecetaExistente_DebeRetornarIndice()
        {
            // Arrange
            GestorRecetas g = new GestorRecetas();

            g.AgregarReceta(new Receta(
                "Paella",
                "Chef",
                45
            ));

            g.AgregarReceta(new Receta(
                "Tacos",
                "Chef",
                20
            ));

            // Act
            int indice =
                g.BusquedaBinaria("paella");

            // Assert
            Assert.True(indice >= 0);
        }

        [Fact]
        public void BusquedaBinaria_RecetaInexistente_DebeRetornarMenosUno()
        {
            // Arrange
            GestorRecetas g = new GestorRecetas();

            g.AgregarReceta(new Receta(
                "Paella",
                "Chef",
                45
            ));

            // Act
            int indice =
                g.BusquedaBinaria(
                    "RecetaXYZInexistente"
                );

            // Assert
            Assert.Equal(-1, indice);
        }
    }
}