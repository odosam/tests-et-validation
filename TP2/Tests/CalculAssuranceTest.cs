using CreditImmobilier.ClassesMetier;
using CreditImmobilier.Services;
using CreditImmobilier.Enumerations;

namespace Tests;

public class CalculAssuranceTest
{
    readonly public decimal capital = 175000m;

    [Fact]
    public void Test_CasToutATrueInge()
    {
        // Cas avec tout à true + profession ingénieur
        CalculAssurance obj = new CalculAssurance();
        Emprunteur client = new Emprunteur()
        {
            EstFumeur = true,
            EstSportif = true,
            TroubleCardiaque = true,
            Profession = Profession.IngenieurInformatique
        };

        decimal tauxAttendu = 0.65m;
        decimal tauxResultat = obj.CalculTotalAssurance(client);

        Assert.Equal(tauxAttendu, tauxResultat, 2);
    }

    [Fact]
    public void Test_CasToutATruePilote()
    {
        // Cas avec tout à true + profession pilote
        CalculAssurance obj = new CalculAssurance();
        Emprunteur client = new Emprunteur()
        {
            EstFumeur = true,
            EstSportif = true,
            TroubleCardiaque = true,
            Profession = Profession.PiloteDeChasse
        }; 

        decimal tauxAttendu = 0.85m;
        decimal tauxResultat = obj.CalculTotalAssurance(client);

        Assert.Equal(tauxAttendu, tauxResultat, 2);
    }

    [Fact]
    public void Test_CasToutAFalse()
    {
        // Cas tout à false + autre 
        CalculAssurance obj = new CalculAssurance();
        Emprunteur client = new Emprunteur()
        {
            EstFumeur = false,
            EstSportif = false,
            TroubleCardiaque = false,
            Profession = Profession.Autre
        };

        decimal tauxAttendu = 0.30m;
        decimal tauxResultat = obj.CalculTotalAssurance(client);

        Assert.Equal(tauxAttendu, tauxResultat, 2);
    }


    [Fact]
    public void Test_MensualiteNormal()
    {
        // Cas avec taux assurance annuel normal avec client "basique"
        Emprunteur client = new Emprunteur();
        client.Profession = CreditImmobilier.Enumerations.Profession.Autre;

        CalculAssurance obj = new CalculAssurance();

        decimal mensuAttendu = 43.75m;
        decimal mensuResultat = obj.CalculMensualiteAssurance(client, capital);

        Assert.Equal(mensuAttendu, mensuResultat, 2);

    }

    [Fact]
    public void Test_MensualiteReductionsOnly()
    {
        // Cas avec que des réductions
        Emprunteur client = new Emprunteur()
        {
            EstFumeur = false,
            EstSportif = true,
            TroubleCardiaque = false,
            Profession = Profession.IngenieurInformatique
        };

        CalculAssurance obj = new CalculAssurance();

        decimal mensuAttendu = 29.17m;
        decimal mensuResultat = obj.CalculMensualiteAssurance(client, capital);

        Assert.Equal(mensuAttendu, mensuResultat, 2);
    }

    [Fact]
    public void Test_MensualitAdditionsOnly()
    {
        // Cas avec que des majorations
        Emprunteur client = new Emprunteur()
        {
            EstFumeur = true,
            EstSportif = false,
            TroubleCardiaque = true,
            Profession = Profession.PiloteDeChasse
        };

        CalculAssurance obj = new CalculAssurance();

        decimal mensuAttendu = 131.25m;
        decimal mensuResultat = obj.CalculMensualiteAssurance(client, capital);

        Assert.Equal(mensuAttendu, mensuResultat, 2);

    }

}
