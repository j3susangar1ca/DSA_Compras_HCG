using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace DSA_Compras_HCG.Views
{
    public sealed partial class NuevoPedidoPage : Page
    {
        public NuevoPedidoPage()
        {
            this.InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // La lógica de los Pilares se activará cuando la base de datos esté lista
        }

        private void ArticuloSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Lógica de búsqueda sub-lineal
        }
    }
}