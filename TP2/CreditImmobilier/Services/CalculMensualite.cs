using CreditImmobilier.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditImmobilier.Services
{
    public class CalculMensualite : ICalculMensualite
    {
        public decimal CalculerMensualite(decimal montant, decimal taux, int dureeMois)
        {
            double mensualite ;

            double tauxMensuel = (double)(taux/100/12); 
            double montantDouble = (double)montant;

            mensualite = (montantDouble * tauxMensuel / (1 - Math.Pow(1 + tauxMensuel,-dureeMois))); 

            return (decimal)mensualite;

        }
        
    }
}
