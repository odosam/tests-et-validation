using CreditImmobilier.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditImmobilier.Interfaces
{
    public interface ICalculTabAmorti
    {
        ResultatSimulation Simulation(DemandePret demande);
    }
}
