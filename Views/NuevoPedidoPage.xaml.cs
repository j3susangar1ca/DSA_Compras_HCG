using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
// Asegúrate de que no haya un 'private' aquí arriba

namespace DSA_Compras_HCG.Views
{
    public sealed partial class NuevoPedidoPage : Page
    {
        public NuevoPedidoPage()
        {
            this.InitializeComponent();
        }

        // Los modificadores private son válidos AQUÍ ADENTRO
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // PILAR 1: Hard-stop de $75,000
            if (txtTotal.Value > 75000)
            {
                // Lógica de error
                return;
            }

            // Validación del Autómata (DFA)
            bool validacionCompleta = chkNegativa.IsChecked == true &&
                                     chkAval.IsChecked == true &&
                                     chkSuPre.IsChecked == true;

            if (!validacionCompleta)
            {
                // Lógica de error
                return;
            }

            // Lógica para guardar
        }

        private void ArticuloSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Lógica de búsqueda
        }
    }
}