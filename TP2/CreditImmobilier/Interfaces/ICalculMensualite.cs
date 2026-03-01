using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditImmobilier.Interfaces
{
    public interface ICalculMensualite
    {
        decimal CalculerMensualite(decimal montant, decimal taux, int dureeMois);
    }
}
