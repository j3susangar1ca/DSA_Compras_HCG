using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using DSA_Compras_HCG.Views;

namespace DSA_Compras_HCG
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // 1. ELIMINAR LA BARRA DE TÍTULO CLÁSICA (Look nativo de Windows 11/10)
            this.ExtendsContentIntoTitleBar = true;

            // 2. Título de la ventana (Aparecerá sutilmente junto a los botones de minimizar/cerrar)
            this.Title = "DSA HCG - Sistema Determinista de Compras";

            // 3. Navegamos al Dashboard al iniciar el programa
            ContentFrame.Navigate(typeof(DashboardPage));
        }
    }
}