using Microsoft.UI.Xaml.Controls;
using DSA_Compras_HCG.ViewModels; // Importamos la carpeta donde vive el cerebro

namespace DSA_Compras_HCG.Views
{
    public sealed partial class DashboardPage : Page
    {
        // AQUÍ ESTÁ LA MAGIA: Creamos la propiedad "ViewModel" que el XAML está buscando
        public DashboardViewModel ViewModel { get; }

        public DashboardPage()
        {
            // Inicializamos el cerebro antes de cargar la pantalla
            ViewModel = new DashboardViewModel();

            this.InitializeComponent();
        }
    }
}