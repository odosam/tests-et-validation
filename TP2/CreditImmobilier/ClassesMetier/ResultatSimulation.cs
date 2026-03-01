using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditImmobilier.ClassesMetier
{
    public class ResultatSimulation
    {
        public decimal PrixGlobalMensualite {  get; set; }  // Big total
        public decimal MensualiteAvecAssurance { get; set; }
        public decimal TotalInterets { get; set; } 
        public decimal TotalAssurance { get; set; }
        public List<LigneAmort> TableauAmortissement { get; set; } // Detail pour chaque mensualite


    }
}
