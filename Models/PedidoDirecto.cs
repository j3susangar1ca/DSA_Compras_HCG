using System;

namespace DSA_Compras_HCG.Models
{
    public class PedidoDirecto
    {
        public int FolioDSA { get; set; }
        public string? TipoTramite { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string? ServicioSolicitante { get; set; }
        public string? EstatusDSA { get; set; }
        public decimal TotalConIva { get; set; }

        // Propiedades calculadas para evitar errores en el Dashboard
        public string FechaFormateada => FechaRecepcion.ToString("dd/MM/yyyy");
        public string TotalFormateado => TotalConIva.ToString("C2");

        // Candados de los Pilares
        public bool NegativaAlmacen { get; set; }
        public bool AvalAdquisiciones { get; set; }
        public bool SuPreVerificado { get; set; }
    }
}