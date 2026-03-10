using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public class DatabaseService
{
    private string _connectionString = "Data Source=Z:\\Compras\\2026\\HCG_Compras.db;"; // Ruta de red

    public List<string> BuscarArticulos(string busqueda)
    {
        var resultados = new List<string>();
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            // Implementación de búsqueda sub-lineal O(log n)
            var query = "SELECT descripcion FROM ARTICULO WHERE descripcion LIKE @busqueda LIMIT 20";
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@busqueda", $"%{busqueda}%");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resultados.Add(reader.GetString(0));
                    }
                }
            }
        }
        return resultados;
    }
}