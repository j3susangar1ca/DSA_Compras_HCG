using System;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.Sqlite;

namespace DSA_Compras_HCG.Services
{
    public class AuditoriaService
    {
        // Ruta a tu carpeta de red compartida en Windows 10
        private readonly string _dbPath = @"Data Source=Z:\Compras\2026\HCG_Compras.db;";

        public void RegistrarEvento(string folioDsa, string tipoEvento, object datosDelPedido, string usuarioActual)
        {
            using (var conn = new SqliteConnection(_dbPath))
            {
                conn.Open();

                // Usamos transacción ACID para no dejar rastros a medias
                using (var transaction = conn.BeginTransaction())
                {
                    string hashAnterior = "BLOQUE_GENESIS_HCG";
                    int versionActual = 1;

                    // 1. Buscar el último evento de este folio para encadenarlo
                    var cmdPrev = conn.CreateCommand();
                    cmdPrev.CommandText = "SELECT hash_integridad, version FROM auditoria_eventos WHERE aggregate_id = @folio ORDER BY version DESC LIMIT 1";
                    cmdPrev.Parameters.AddWithValue("@folio", folioDsa);

                    using (var reader = cmdPrev.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hashAnterior = reader.GetString(0);
                            versionActual = reader.GetInt32(1) + 1; // Incrementamos la versión
                        }
                    }

                    // 2. Serializar la evidencia en JSON
                    string eventDataJson = JsonSerializer.Serialize(datosDelPedido);
                    string metadataJson = JsonSerializer.Serialize(new { Usuario = usuarioActual, Plataforma = "WinUI 3" });
                    string timestamp = DateTime.UtcNow.ToString("O"); // ISO 8601

                    // 3. Generar el sello criptográfico SHA-256 (El Blockchain Ligero)
                    string rawData = $"{hashAnterior}{eventDataJson}{timestamp}";
                    string nuevoHash = GenerarSHA256(rawData);

                    // 4. Guardar en la base de datos
                    var cmdInsert = conn.CreateCommand();
                    cmdInsert.CommandText = @"
                        INSERT INTO auditoria_eventos 
                        (aggregate_type, aggregate_id, event_type, event_data, metadata, version, created_at, hash_integridad)
                        VALUES ('PEDIDO_DIRECTO', @id, @tipo, @data, @meta, @version, @time, @hash)";

                    cmdInsert.Parameters.AddWithValue("@id", folioDsa);
                    cmdInsert.Parameters.AddWithValue("@tipo", tipoEvento); // ej. 'SOLICITUD_CREADA'
                    cmdInsert.Parameters.AddWithValue("@data", eventDataJson);
                    cmdInsert.Parameters.AddWithValue("@meta", metadataJson);
                    cmdInsert.Parameters.AddWithValue("@version", versionActual);
                    cmdInsert.Parameters.AddWithValue("@time", timestamp);
                    cmdInsert.Parameters.AddWithValue("@hash", nuevoHash);

                    cmdInsert.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

        // Función matemática criptográfica
        private string GenerarSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}