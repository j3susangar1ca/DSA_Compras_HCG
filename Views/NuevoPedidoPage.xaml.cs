using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace DSA_Compras_HCG.Views
{
    public sealed partial class NuevoPedidoPage : Page
    {
        public NuevoPedidoPage()
        {
            // Si esto marca línea roja, no te preocupes, se quitará al recompilar
            this.InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Dejamos el botón funcional pero sin lógica compleja por ahora
            // para evitar errores de controles no reconocidos.
        }

        private void ArticuloSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Buscador vacío por ahora
        }
    }
}