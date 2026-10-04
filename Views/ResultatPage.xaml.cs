	namespace Calculatrice.Views;

	// Relie le parametre "nom" de l'URl a la propriete Nom
	[QueryProperty(nameof(Nom), "nom")]
	[QueryProperty(nameof(Age), "age")]
	public partial class ResultatPage : ContentPage
	{
		// Ces pptes sont remplies par la navigation,
		// Apres le constructeur
		public string Nom { get; set; }
		public string Age { get; set; }

		//Construit l arbre visuel decrit par le XAML
		public ResultatPage() => InitializeComponent();

		//Appelle a Chaque Affichage de la page
		protected override void OnAppearing()
		{
			base.OnAppearing();
			lblMessage.Text = $"{Nom}, vous avez {Age} ans";
		}

		// ".." = revenir a la page precedente.
		private async void OnRetourClicked(object s, EventArgs e)
    				=> await Shell.Current.GoToAsync("..");
	}

