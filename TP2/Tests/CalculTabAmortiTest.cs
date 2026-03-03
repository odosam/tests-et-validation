using CreditImmobilier.ClassesMetier;
using CreditImmobilier.Enumerations;
using CreditImmobilier.Services;
using System.Linq;

namespace Tests;

public class CalculTabAmortiTest
{
    [Fact]
    public void Test_NombreLignesTableau()
    {
        Emprunteur profil = new Emprunteur();
        profil.Profession = Profession.Autre;

        DemandeCredit demande = new DemandeCredit()
        {
            Montant = 200000m,
            DureeAnnees = 15,
            QualiteTaux = QualiteTaux.TresBon,
            Profil = profil
        };

        CalculAssurance calcAss = new CalculAssurance();
        CalculMensualite calculMens = new CalculMensualite();

        CalculTabAmorti obj = new CalculTabAmorti(calculMens,calcAss);

        ResultatSimulation nombreResultat = obj.Simulation(demande);
        int nombreAttendu = 180;

        Assert.Equal(nombreAttendu, nombreResultat.TableauAmortissement.Count);

    }

    [Fact]
    public void Test_CapitalFinal()
    {
        Emprunteur profil = new Emprunteur();
        profil.Profession = Profession.IngenieurInformatique;

        DemandeCredit demande = new DemandeCredit()
        {
            Montant = 200000m,
            DureeAnnees = 15,
            QualiteTaux = QualiteTaux.TresBon,
            Profil = profil
        };

        CalculAssurance calcAss = new CalculAssurance();
        CalculMensualite calculMens = new CalculMensualite();

        CalculTabAmorti obj = new CalculTabAmorti(calculMens, calcAss);

        ResultatSimulation nombreResultat = obj.Simulation(demande);
        decimal nombreAttendu = 0.00m;

        Assert.Equal(nombreAttendu, nombreResultat.TableauAmortissement.Last().ResteAPayer,2);
    }

    [Fact]
    public void Test_PartCapitalEgalMontant()
    {
        Emprunteur profil = new Emprunteur();
        profil.Profession = Profession.IngenieurInformatique;

        DemandeCredit demande = new DemandeCredit()
        {
            Montant = 200000m,
            DureeAnnees = 15,
            QualiteTaux = QualiteTaux.TresBon,
            Profil = profil
        };

        CalculAssurance calcAss = new CalculAssurance();
        CalculMensualite calculMens = new CalculMensualite();

        CalculTabAmorti obj = new CalculTabAmorti(calculMens, calcAss);

        ResultatSimulation nombreResultat = obj.Simulation(demande);
        decimal nombreAttendu = demande.Montant;

        Assert.Equal(nombreAttendu, nombreResultat.TableauAmortissement.Sum(l => l.PartPayee), 2);
    }

    [Fact]
    public void Test_PartPayeApres10Ans()
    {
        Emprunteur profil = new Emprunteur();
        profil.Profession = Profession.IngenieurInformatique;

        DemandeCredit demande = new DemandeCredit()
        {
            Montant = 200000m,
            DureeAnnees = 15,
            QualiteTaux = QualiteTaux.TresBon,
            Profil = profil
        };

        CalculAssurance calcAss = new CalculAssurance();
        CalculMensualite calculMens = new CalculMensualite();

        CalculTabAmorti obj = new CalculTabAmorti(calculMens, calcAss);

        ResultatSimulation nombreResultat = obj.Simulation(demande);
        decimal nombreAttendu = 130886.49m;

        Assert.Equal(nombreAttendu, nombreResultat.TableauAmortissement.Take(120).Sum(l => l.PartPayee), 2);
    }
}
