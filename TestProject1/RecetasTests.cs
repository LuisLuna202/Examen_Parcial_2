using Xunit;
using SistemaRecetas.Modelos;


namespace SistemaRecetas.Test
{
    public class RecetaTests
    {
        // 1. Prueba del constructor
        [Fact]
        public void ConstructorDebeAsignarPropiedadesCorrectamente()
        {
            // Arrange (de cierta forma, tambien implicitamente siendo el act)
            Receta receta = new Receta(
                "Paella",
                "Chef Ramírez",
                45
            );

            // Assert
            Assert.Equal("Paella", receta.Nombre);
            Assert.Equal("Chef Ramírez", receta.Chef);
            Assert.Equal(45, receta.TiempoMinutos);
        }

        // 2. Prueba de ToString()
        [Fact]
        public void ToString_DebeRetornarFormatoCorrecto()
        {
            // Arrange
            Receta receta = new Receta(
                "Paella",
                "Chef Ramírez",
                45
            );

            // Act
            string resultado = receta.ToString();

            // Assert
            Assert.Equal(
                "Paella - Chef Ramírez (45 min)",
                resultado
            );
        }

        // 3. TiempoMinutos negativo
        [Fact]
        public void Constructor_TiempoNegativo_DebeLanzarException()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
                new Receta("Test", "Chef", -1)
            );
        }

        // 4. TiempoMinutos igual a cero
        [Fact]
        public void Constructor_TiempoCero_DebeLanzarException()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
                new Receta("Test", "Chef", 0)
            );
        }
    }
}