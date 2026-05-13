using ConsoleApp1.Modelos;
using SistemaRecetas.Modelos;

namespace SistemaRecetas.Interfaces
{
    public interface IExportador
    {
        void ExportarATxt(Usuario usuario, string rutaArchivo);
    }
}