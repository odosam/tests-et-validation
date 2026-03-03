using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditImmobilier.ClassesMetier
{
    public class ResultatSimulation
    {
        public decimal PrixGlobalMensualite {  get; set; }  
        public decimal MensualiteAvecAssurance { get; set; }
        public decimal TotalInterets { get; set; } 
        public decimal TotalAssurance { get; set; }
        public List<LigneAmortissement> TableauAmortissement { get; set; } // Detail pour chaque mensualite


    }
}
