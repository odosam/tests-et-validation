namespace CreditImmobilier.ClassesMetier
{
    public class LigneAmortissement
    {
        public int Mois { get; set; }
        public decimal MensualiteTotale { get; set; } 
        public decimal Interets { get; set; }        
        public decimal PartPayee { get; set; }        // Ce qui est payé durant ce mois ci 
        public decimal ResteAPayer { get; set; }      // Ce qui reste à payer sur la totalité du prêt
    }
}