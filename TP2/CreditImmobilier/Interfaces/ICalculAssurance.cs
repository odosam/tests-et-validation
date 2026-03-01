using CreditImmobilier.Enumerations;
using CreditImmobilier.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditImmobilier.Interfaces
{
    internal interface ICalculAssurance
    {
        decimal CalculTotalAssurance(Emprunteur client);
        decimal CalculMensualiteAssurance(Emprunteur client); // TODO : réflechir comment faire
    }
}
