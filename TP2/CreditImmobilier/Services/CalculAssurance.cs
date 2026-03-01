using CreditImmobilier.Interfaces;
using CreditImmobilier.ClassesMetier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditImmobilier.Services
{
    public class CalculAssurance : ICalculAssurance
    {
        public decimal CalculTotalAssurance(Emprunteur client)
        {
            double taux = 0.30;

            if (client.Profession == Enumerations.Profession.IngenieurInformatique)
            {
                taux -= 0.05;  
            }
            else if (client.Profession == Enumerations.Profession.PiloteDeChasse)
            {
                taux += 0.15;
            }
            
            if (client.EstFumeur)
            {
                taux += 0.15;
            }

            if (client.EstSportif)
            {
                taux -= 0.05;
            }

            if (client.TroubleCardiaque)
            {
                taux += 0.3;
            }

           return (decimal)taux;
        }

        public decimal CalculMensualiteAssurance(Emprunteur client, decimal capital)
        {
            decimal tauxAssuranceAnnuel = CalculTotalAssurance(client);
            
            decimal mensualiteAssurance = capital * (tauxAssuranceAnnuel/100)/12 ;

            return mensualiteAssurance;
        }

        
    }
}
