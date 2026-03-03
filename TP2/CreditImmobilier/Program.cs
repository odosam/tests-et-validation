using CreditImmobilier.Enumerations;
using CreditImmobilier.Interfaces;
using CreditImmobilier.ClassesMetier;
using CreditImmobilier.Services;

// Saisie des données
Console.WriteLine("=== Simulateur de crédit immobilier ===\n");

Console.Write("Capital emprunté (min 50 000€) : ");
decimal capital = decimal.Parse(Console.ReadLine());

Console.Write("Durée en années (entre 9 et 25) : ");
int dureeEnAnnees = int.Parse(Console.ReadLine());

// Verification des données
if (capital < 50000)
{
    Console.WriteLine("Erreur : le capital doit être supérieur à 50 000 Euro ");
    return;
}
if (dureeEnAnnees < 9 || dureeEnAnnees > 25)
{
    Console.WriteLine("Erreur : la durée doit être entre 9 et 25 ans");
    return;
}

Console.WriteLine("\nQualité du taux ? (1 = Bon, 2 = Très bon, 3 = Excellent)");
int choixTaux = int.Parse(Console.ReadLine());
QualiteTaux qualiteTaux = choixTaux switch
{
    1 => QualiteTaux.Bon,
    2 => QualiteTaux.TresBon,
    3 => QualiteTaux.Excellent,
    _ => QualiteTaux.Bon
};

Console.WriteLine("\n=== Profil emprunteur ===");
Console.Write("Fumeur ? (o/n) : ");
bool estFumeur = Console.ReadLine().ToLower() == "o";

Console.Write("Sportif ? (o/n) : ");
bool estSportif = Console.ReadLine().ToLower() == "o";

Console.Write("Trouble cardiaque ? (o/n) : ");
bool aTroubleCardiaque = Console.ReadLine().ToLower() == "o";

Console.WriteLine("Profession ? (1 = Autre, 2 = Ingénieur informatique, 3 = Pilote de chasse)");
int choixProfession = int.Parse(Console.ReadLine());
Profession profession = choixProfession switch
{
    2 => Profession.IngenieurInformatique,
    3 => Profession.PiloteDeChasse,
    _ => Profession.Autre
};

// Construction des objets
var profil = new Emprunteur
{
    EstFumeur = estFumeur,
    EstSportif = estSportif,
    TroubleCardiaque = aTroubleCardiaque,
    Profession = profession
};

var demande = new DemandeCredit
{
    Montant = capital,
    DureeAnnees = dureeEnAnnees,
    QualiteTaux = qualiteTaux,
    Profil = profil
};

// Simulation
var service = new CalculTabAmorti(
    new CalculMensualite(),
    new CalculAssurance()
);

var resultat = service.Simulation(demande);

// Affichage des résultats
Console.WriteLine("\n=== Résultats ===");
Console.WriteLine($"Mensualité globale         : {resultat.PrixGlobalMensualite:F2}E");
Console.WriteLine($"Mensualité hors assurance  : {resultat.PrixGlobalMensualite - resultat.MensualiteAvecAssurance:F2}€");
Console.WriteLine($"Cotisation assurance/mois  : {resultat.MensualiteAvecAssurance:F2}E");
Console.WriteLine($"Total des intérêts         : {resultat.TotalInterets:F2}E");
Console.WriteLine($"Total assurance            : {resultat.TotalAssurance:F2}E");

// Capital remboursé après 10 ans
if (dureeEnAnnees > 10)
{
    decimal capitalRembourse = resultat.TableauAmortissement
        .Take(120)
        .Sum(l => l.PartPayee);
    Console.WriteLine($"Capital remboursé après 10 ans : {capitalRembourse:F2}€");
}

Console.WriteLine("\n=== Tableau d'amortissement ===");
Console.WriteLine($"{"Mois",-6} {"Mensualité",-12} {"Intérêts",-12} {"Capital",-12} {"Capital restant",-15}");
Console.WriteLine(new string('-', 57));

foreach (var ligne in resultat.TableauAmortissement)
{
    Console.WriteLine($"{ligne.Mois,-6} {ligne.MensualiteTotale,-12:F2} {ligne.Interets,-12:F2} {ligne.PartPayee,-12:F2} {ligne.ResteAPayer,-15:F2}");
}
