using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditImmobilier.Modeles
{
    public class ResultatSimulation
    {
        public decimal PrixGlobalMensualite {  get; set; }  // Big total
        public decimal CotisationMensuelleAssurance { get; set; }
        public decimal TotalInterets { get; set; } 
        public decimal TotalAssurance { get; set; }
        public decimal CapitalRembourseEnCours { get; set; } // => Evolutif 
        public List<LigneAmort> TableauAmortissement { get; set; } // Detail pour chaque mensualite


    }
}
