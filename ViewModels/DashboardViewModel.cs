using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DSA_Compras_HCG.ViewModels
{
    // Heredamos de ObservableObject para que la UI se entere de los cambios
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        // ====================================================================
        // 1. PROPIEDADES OBSERVABLES (TARJETAS DEL DASHBOARD)
        // ====================================================================
        // Al usar [ObservableProperty], el toolkit genera automáticamente el código 
        // para actualizar el XAML cuando estos valores cambien.

        [ObservableProperty]
        private string _totalRegistroInicial = "0";

        [ObservableProperty]
        private string _totalPendientesValidacion = "0";

        [ObservableProperty]
        private string _totalEnCotizacion = "0";

        [ObservableProperty]
        private string _totalPagados = "0";

        // ====================================================================
        // 2. COLECCIONES Y FILTROS PARA EL DATAGRID
        // ====================================================================

        [ObservableProperty]
        private ObservableCollection<PedidoDirecto> _foliosFiltrados = new();

        private List<PedidoDirecto> _todosLosFolios = new();

        [ObservableProperty]
        private string _textoBusqueda = string.Empty;

        // ====================================================================
        // 3. CONSTRUCTOR
        // ====================================================================
        public DashboardViewModel()
        {
            _dbService = new DatabaseService(); // Nuestro servicio a SQLite
            _ = CargarDatosDashboardAsync();    // Cargamos asíncronamente
        }

        // ====================================================================
        // 4. LÓGICA DE NEGOCIO Y CONSULTA A SQLITE
        // ====================================================================
        private async Task CargarDatosDashboardAsync()
        {
            // Simulamos una carga asíncrona para no congelar la UI de WinUI 3
            // En la vida real, _dbService.ObtenerTodosLosPedidos() hace un SELECT a SQLite.
            _todosLosFolios = await Task.Run(() => _dbService.ObtenerTodosLosPedidos());

            // Actualizamos el DataGrid
            FoliosFiltrados = new ObservableCollection<PedidoDirecto>(_todosLosFolios);

            // Calculamos las métricas del Autómata (DFA) usando LINQ
            // Estado S0: Registro Inicial
            TotalRegistroInicial = _todosLosFolios
                .Count(f => f.EstatusDSA == "REGISTRO_INICIAL").ToString();

            // Estado S1 y S2: Esperando Negativa (Pilar 3) o Aval (Pilar 2)
            TotalPendientesValidacion = _todosLosFolios
                .Count(f => !f.NegativaAlmacen || (!f.AvalAdquisiciones && f.TipoTramite == "COMPRA POR FONDO")).ToString();

            // Estado S4: En proceso de Estudio de Mercado (Pilar 5)
            TotalEnCotizacion = _todosLosFolios
                .Count(f => f.EstatusDSA == "COTIZACION").ToString();

            // Estado S11: Pagado y listo para Reintegro (Pilar 8)
            TotalPagados = _todosLosFolios
                .Count(f => f.EstatusDSA == "PAGADO").ToString();
        }

        // Método que se ejecuta cada vez que el usuario escribe en la barra de búsqueda
        partial void OnTextoBusquedaChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                FoliosFiltrados = new ObservableCollection<PedidoDirecto>(_todosLosFolios);
            }
            else
            {
                var filtro = value.ToLower();
                var resultados = _todosLosFolios.Where(f =>
                    f.FolioDSA.ToString().Contains(filtro) ||
                    (f.ServicioSolicitante != null && f.ServicioSolicitante.ToLower().Contains(filtro))
                );
                FoliosFiltrados = new ObservableCollection<PedidoDirecto>(resultados);
            }
        }

        // ====================================================================
        // 5. COMANDOS (ACCIONES DE LOS BOTONES EN XAML)
        // ====================================================================

        [RelayCommand]
        private void IrANuevoPedido()
        {
            // Lógica para navegar a la página de Nuevo Pedido
            // Frame.Navigate(typeof(NuevoPedidoPage));
        }

        [RelayCommand]
        private void GestionarFolio(int folioDsa)
        {
            // Lógica para abrir el detalle de un folio específico
            // El usuario hizo clic en "Gestionar" en el DataGrid
        }
    }
}