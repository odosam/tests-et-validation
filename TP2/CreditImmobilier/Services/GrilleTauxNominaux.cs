using CreditImmobilier.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditImmobilier.Services
{
    public static class GrilleTauxNominaux
    {
        public static decimal ObtentionTaux(QualiteTaux qualite, int dureeAnnees)
        {
            if(qualite == QualiteTaux.Bon)
            {
                if (dureeAnnees <= 7)
                    return 0.62m;
                if (dureeAnnees <= 10)
                    return 0.67m;
                if (dureeAnnees <= 15)
                    return 0.85m;
                if (dureeAnnees <= 20)
                    return 1.04m;
                if (dureeAnnees <= 25)
                    return 1.27m;
                else
                    return 1.27m;
            }

            if (qualite == QualiteTaux.TresBon)
            {
                if (dureeAnnees <= 7)
                    return 0.43m;
                if (dureeAnnees <= 10)
                    return 0.55m;
                if (dureeAnnees <= 15)
                    return 0.73m;
                if (dureeAnnees <= 20)
                    return 0.91m;
                if (dureeAnnees <= 25)
                    return 1.25m;
                else
                    return 1.15m;
            }

            if (dureeAnnees <= 7)
                return 0.35m;
            if (dureeAnnees <= 10)
                return 0.45m;
            if (dureeAnnees <= 15)
                return 0.58m;
            if (dureeAnnees <= 20)
                return 0.73m;
            else
                return 0.89m;
            
        }
    }
}
