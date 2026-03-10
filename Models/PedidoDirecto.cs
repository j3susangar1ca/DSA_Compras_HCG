using System;

namespace DSA_Compras_HCG.Models
{
    // Refleja exactamente tu tabla de SQLite y los Pilares Normativos
    public class PedidoDirecto
    {
        public int FolioDSA { get; set; }
        public string TipoTramite { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string ServicioSolicitante { get; set; }
        public string EstatusDSA { get; set; }
        public decimal TotalConIva { get; set; }

        // Pilares Normativos
        public bool NegativaAlmacen { get; set; }
        public bool AvalAdquisiciones { get; set; }
        public bool SuPreVerificado { get; set; }

        // Datos Clínicos
        public string PacienteId { get; set; }
        public string MedicoTratanteId { get; set; }
    }
}