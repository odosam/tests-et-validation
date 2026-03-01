namespace CreditImmobilier.Modeles
{
    public class LigneAmort
    {
        // Permet d'avoir une liste pour chaque mois avec dedans les interets, capital, et ce qui reste à payer
        public int Mois { get; set; }
        public decimal MensualiteTotale { get; set; } // Prix total de ce qui est payé durant le mois
        public decimal Interets { get; set; }         // Interets de la banque
        public decimal PartPayee { get; set; }        // Ce qui est payé durant ce mois ci 
        public decimal ResteAPayer { get; set; }      // Ce qui reste à payer sur la totalité du prêt
    }
}