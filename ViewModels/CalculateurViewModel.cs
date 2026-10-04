using System.Collections.ObjectModel;

namespace Calculatrice.ViewModels;

public class CalculatriceViewModel : BaseViewModel
{
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);

    private string _resultat = "";
    private bool _resultatVisible;
    private string _joursRestants = "";

    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
                CalculerCommand.Rafraichir();
        }
    }

    // Date maximale autorisée : aujourd'hui
    public DateTime DateMax => DateTime.Today;

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set
        {
            // Garde-fou : une date future est ramenée à aujourd'hui
            if (value > DateMax)
                value = DateMax;

            SetField(ref _dateNaissance, value);
        }
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    // Fonctionnalité 2 : compte à rebours avant l'anniversaire
    public string JoursRestants
    {
        get => _joursRestants;
        set => SetField(ref _joursRestants, value);
    }

    // Fonctionnalité 3 : historique des calculs
    // ObservableCollection prévient la liste à chaque Insert/Clear.
    public ObservableCollection<string> Historique { get; } = new();

    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }           // Fonctionnalité 1
    public RelayCommand ViderHistoriqueCommand { get; }

    public CalculatriceViewModel()
    {
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom));
        EffacerCommand = new RelayCommand(Effacer);
        ViderHistoriqueCommand = new RelayCommand(() => Historique.Clear());
    }

    private void Calculer()
    {
        int age = DateTime.Today.Year - DateNaissance.Year;
        if (DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";
        JoursRestants = CalculerJoursRestants();
        ResultatVisible = true;

        // Le plus récent en haut de la liste
        Historique.Insert(0,
            $"{Nom} : {age} ans ({DateTime.Now:dd/MM/yyyy HH:mm})");
    }

    // Nombre de jours avant le prochain anniversaire
    private string CalculerJoursRestants()
    {
        DateTime aujourdhui = DateTime.Today;
        DateTime prochain = AnniversaireEn(aujourdhui.Year);

        if (prochain < aujourdhui)
            prochain = AnniversaireEn(aujourdhui.Year + 1);

        int jours = (prochain - aujourdhui).Days;

        return jours == 0
            ? "Joyeux anniversaire !"
            : $"Prochain anniversaire dans {jours} jour(s)";
    }

    // Gère le 29 février : en année non bissextile, on prend le 28.
    private DateTime AnniversaireEn(int annee)
    {
        int jour = DateNaissance.Day;
        if (DateNaissance.Month == 2 && jour == 29
            && !DateTime.IsLeapYear(annee))
            jour = 28;

        return new DateTime(annee, DateNaissance.Month, jour);
    }

    // Remet tous les champs à zéro (l'historique est conservé)
    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        JoursRestants = "";
        ResultatVisible = false;
    }
}