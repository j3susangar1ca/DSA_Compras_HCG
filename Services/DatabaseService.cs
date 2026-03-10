using System.Collections.Generic;
using DSA_Compras_HCG.Models; // Necesario para reconocer la clase PedidoDirecto

namespace DSA_Compras_HCG.Services
{
    public class DatabaseService
    {
        // Solución al Error: Creamos el método que pide el Dashboard
        public List<PedidoDirecto> ObtenerTodosLosPedidos()
        {
            // Por ahora devolvemos una lista vacía. 
            // Más adelante aquí pondremos la conexión real a SQLite.
            return new List<PedidoDirecto>();
        }

        // Dejamos el buscador que creamos antes, también vacío por ahora
        public List<string> BuscarArticulos(string busqueda)
        {
            return new List<string>();
        }
    }
}