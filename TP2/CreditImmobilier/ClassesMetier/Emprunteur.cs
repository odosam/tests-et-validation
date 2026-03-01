using CreditImmobilier.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditImmobilier.Modeles
{
    public class Emprunteur
    {
        public bool EstFumeur { get; set; }
        public bool EstSportif {  get; set; }
        public bool TroubleCardiaque { get; set; }
        public Profession Profession { get; set; }

    }
}
