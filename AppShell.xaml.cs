using Calculatrice.Views;

namespace Calculatrice;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		// Declare la route : sans cette lige, GoToAsync leve une exception "route inconnue"
		Routing.RegisterRoute(nameof(Views.ResultatPage), typeof(Views.ResultatPage));
	}
}
