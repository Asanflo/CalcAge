using Calculatrice.ViewModels;

namespace Calculatrice;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new CalculatriceViewModel();
    }
}