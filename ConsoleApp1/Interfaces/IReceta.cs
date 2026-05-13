
﻿public interface IReceta
﻿
namespace SistemaRecetas.Interfaces

{
    public interface IReceta
    {
        public string Nombre { get; set; }
        public string Chef { get; set; }
        public int TiempoMinutos { get; set; }
        public string ToString()
        {
            return ToString();
        }
    }
}