
using CreditImmobilier.Services;
using System.Reflection.Metadata;

namespace Tests
{
    public class CalculMensualiteTest
    {
        [Fact]
        public void Test_CasNormal()
        {
            decimal montant = 175000;
            decimal taux = 0.85m; 
            int dureeMois = 132; // 11 ans

            CalculMensualite obj = new CalculMensualite();
            
            decimal mensualiteCalculee = obj.CalculerMensualite(montant,taux,dureeMois);
            
            // Arrondi au dixieme superieur
            decimal mensualiteAttendue = 1389.17m;

            Assert.Equal(mensualiteAttendue,mensualiteCalculee,2);

        }

        [Fact]
        public void Test_Limite9Ans()
        {
            decimal montant = 175000;
            decimal taux = 0.67m;
            int dureeMois = 108;

            decimal mensualiteAttendue = 1670.17m;

            CalculMensualite obj = new CalculMensualite();
            decimal mensualiteCalculee = obj.CalculerMensualite(montant, taux, dureeMois);

            Assert.Equal(mensualiteAttendue, mensualiteCalculee, 2);
        }

        [Fact]
        public void Test_Limite25Ans()
        {
            decimal montant = 175000;
            decimal taux = 1.27m;
            int dureeMois = 300; //25 ans

            CalculMensualite obj = new CalculMensualite();

            decimal mensualiteCalculee = obj.CalculerMensualite(montant, taux, dureeMois);

            // Arrondi au dixieme superieur
            decimal mensualiteAttendue = 681.14m;

            Assert.Equal(mensualiteAttendue, mensualiteCalculee, 2);
        }

        [Fact]
        public void Test_CasImpossibleCapital0()
        {
            // On garde le Assert.Equal car mathématiquement juste 
            decimal montant = 0;
            decimal taux = 1.27m;
            int dureeMois = 300; //25 ans

            CalculMensualite obj = new CalculMensualite();

            decimal mensualiteCalculee = obj.CalculerMensualite(montant, taux, dureeMois);

            decimal mensualiteAttendue = 0.00m;

            Assert.Equal(mensualiteAttendue, mensualiteCalculee, 2);

        }

        [Fact]
        public void Test_CasImpossibleTaux0()
        {
            decimal montant = 175000;
            decimal taux = 0m;
            int dureeMois = 300; //25 ans

            CalculMensualite obj = new CalculMensualite();

            Assert.Throws<OverflowException>(() => obj.CalculerMensualite(montant, taux, dureeMois));

        }

        [Fact]
        public void Test_CasImpossibleMois0()
        {
            decimal montant = 175000;
            decimal taux = 1.27m;
            int dureeMois = 0; 

            CalculMensualite obj = new CalculMensualite();

            Assert.Throws<OverflowException>(() => obj.CalculerMensualite(montant, taux, dureeMois));

        }
    }
}