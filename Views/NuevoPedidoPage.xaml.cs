using Microsoft.UI.Xaml;

private void btnGuardar_Click(object sender, RoutedEventArgs e)
{
    // PILAR 1: Hard-stop de $75,000
    if (txtTotal.Value > 75000)
    {
        MostrarError("Pilar 1: Excede el límite de Fondo Revolvente.");
        return;
    }

    // Validación del Autómata (DFA)
    bool validacionCompeta = chkNegativa.IsChecked == true &&
                             chkAval.IsChecked == true &&
                             chkSuPre.IsChecked == true;

    if (!validacionCompeta)
    {
        MostrarError("Faltan validaciones normativas obligatorias.");
        return;
    }

    // Si todo es correcto, procedemos al INSERT en SQLite
    GuardarEnBaseDeDatos();
}