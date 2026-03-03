using CreditImmobilier.ClassesMetier;
using CreditImmobilier.Services;
using NuGet.Frameworks;

namespace Tests;

public class CalculAssuranceTest
{
    readonly public decimal capital = 175000m;

    [Fact]
    public void Test_CasToutTotal()
    {
        // Cas avec tout à true + profession ingénieur
        CalculAssurance obj = new CalculAssurance();
        Emprunteur client = new Emprunteur();

        decimal tauxAttendu = 0.65m;

        client.EstFumeur = true;
        client.EstSportif = true;
        client.TroubleCardiaque = true;
        client.Profession = CreditImmobilier.Enumerations.Profession.IngenieurInformatique;

        decimal tauxResultat = obj.CalculTotalAssurance(client);

        Assert.Equal(tauxAttendu, tauxResultat, 2);
    }

    [Fact]
    public void Test_CasToutTotal2()
    {
        // Cas avec tout à true + profession pilote
        CalculAssurance obj = new CalculAssurance();
        Emprunteur client = new Emprunteur();

        decimal tauxAttendu = 0.85m;
        client.EstFumeur = true;
        client.EstSportif = true;
        client.TroubleCardiaque = true;
        client.Profession = CreditImmobilier.Enumerations.Profession.PiloteDeChasse;

        decimal tauxResultat = obj.CalculTotalAssurance(client);

        Assert.Equal(tauxAttendu, tauxResultat, 2);
    }

    [Fact]
    public void Test_CasNormalTotal()
    {
        // Cas tout à false + autre 
        CalculAssurance obj = new CalculAssurance();
        Emprunteur client = new Emprunteur();

        decimal tauxAttendu = 0.30m;
        client.EstFumeur = false;
        client.EstSportif = false;
        client.TroubleCardiaque = false;
        client.Profession = CreditImmobilier.Enumerations.Profession.Autre;

        decimal tauxResultat = obj.CalculTotalAssurance(client);

        Assert.Equal(tauxAttendu, tauxResultat, 2);
    }

    [Fact]
    public void Test_CasReductionTotal()
    {
        // Cas avec que des réductions
        CalculAssurance obj = new CalculAssurance();
        Emprunteur client = new Emprunteur();

        decimal tauxAttendu = 0.20m;
        client.EstFumeur = false;
        client.EstSportif = true;
        client.TroubleCardiaque = false;
        client.Profession = CreditImmobilier.Enumerations.Profession.IngenieurInformatique;

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
        Emprunteur client = new Emprunteur();
        client.EstFumeur = false;
        client.EstSportif = true;
        client.TroubleCardiaque = false;
        client.Profession = CreditImmobilier.Enumerations.Profession.IngenieurInformatique;


        CalculAssurance obj = new CalculAssurance();

        decimal mensuAttendu = 29.17m;
        decimal mensuResultat = obj.CalculMensualiteAssurance(client, capital);

        Assert.Equal(mensuAttendu, mensuResultat, 2);
    }

    [Fact]
    public void Test_MensualitAdditionsOnly()
    {
        // Cas avec que des majorations
        Emprunteur client = new Emprunteur();
        client.EstFumeur = true;
        client.EstSportif = false;
        client.TroubleCardiaque = true;
        client.Profession = CreditImmobilier.Enumerations.Profession.PiloteDeChasse;


        CalculAssurance obj = new CalculAssurance();

        decimal mensuAttendu = 131.25m;
        decimal mensuResultat = obj.CalculMensualiteAssurance(client, capital);

        Assert.Equal(mensuAttendu, mensuResultat, 2);

    }

    
}
