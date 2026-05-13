
using Xunit;
using SistemaRecetas.Servicios;

namespace SistemaRecetas.Test
{
    public class ServicioRecetasTests
    {
        private readonly ServicioRecetas _servicio;

        public ServicioRecetasTests()
        {
            // Dependencias necesarias
            IGestorRecetas gestor = new GestorRecetas();
            IExportador exportador = new ExportadorTXT();

            _servicio = new ServicioRecetas(
                gestor,
                exportador
            );
        }

        // 1. RegistrarUsuario()
        [Fact]
        public void RegistrarUsuario_DebeAgregarUsuario()
        {
            // Act
            _servicio.RegistrarUsuario("TestUser");

            // Assert
            Assert.NotNull(
                _servicio.BuscarUsuario("TestUser")
            );

            Assert.Equal(
                1,
                _servicio.ContarUsuarios()
            );
        }

        // 2. BuscarUsuario()
        [Fact]
        public void BuscarUsuario_DebeIgnorarMayusculas()
        {
            // Arrange
            _servicio.RegistrarUsuario("Ana");

            // Act
            Usuario usuario =
                _servicio.BuscarUsuario("ANA");

            // Assert
            Assert.NotNull(usuario);
        }

        // Usuario inexistente
        [Fact]
        public void BuscarUsuario_Inexistente_DebeRetornarNull()
        {
            // Act
            Usuario usuario =
                _servicio.BuscarUsuario("Inexistente");

            // Assert
            Assert.Null(usuario);
        }

        // 3. ExportarLibros()
        [Fact]
        public void ExportarLibros_DebeCrearArchivoTXT()
        {
            // Arrange
            Usuario usuario =
                _servicio.RegistrarUsuario("Carlos");

            usuario.CrearLibroRecetas("Favoritas");

            Receta receta = new Receta(
                "Paella",
                "Chef Ramírez",
                45
            );

            usuario.AgregarRecetaALibro(
                "Favoritas",
                receta
            );

            string ruta = "prueba_recetas.txt";

            // Act
            _servicio.Exportador.Exportar(
                usuario,
                ruta
            );

            // Assert
            Assert.True(
                File.Exists(ruta)
            );

            string contenido =
                File.ReadAllText(ruta);

            Assert.Contains(
                "Paella",
                contenido
            );

            // Limpieza opcional
            if (File.Exists(ruta))
            {
                File.Delete(ruta);
            }
        }
    }
}