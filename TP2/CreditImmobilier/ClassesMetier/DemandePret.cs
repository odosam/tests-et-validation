using CreditImmobilier.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditImmobilier.Modeles
{
    public class DemandePret
    {
        public decimal Montant { get; set; }
        public int DureeAnnees { get; set; }
        public QualiteTaux QualiteTaux { get; set; }
        public Emprunteur Profil { get; set; }
        public int DureeMois
        {
            get { return DureeAnnees * 12; }

        }


    }
}
