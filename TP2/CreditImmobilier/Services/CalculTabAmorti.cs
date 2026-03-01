using CreditImmobilier.Interfaces;
using CreditImmobilier.ClassesMetier;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditImmobilier.Services
{
    public class CalculTabAmorti : ICalculTabAmorti
    {
        private ICalculMensualite _calculMensualite;
        private ICalculAssurance _calculAssurance;

        public CalculTabAmorti( ICalculMensualite calculMensualite, ICalculAssurance calculAssurance)
        {
            _calculMensualite = calculMensualite;
            _calculAssurance = calculAssurance;
        }

        public ResultatSimulation Simulation(DemandeCredit demande)
        {

            // Calcul mensualite
            decimal tauxAnnuel = GrilleTauxNominaux.ObtentionTaux(demande.QualiteTaux, demande.DureeAnnees);

            // Mensualité sans assurance
            decimal mensualite = _calculMensualite.CalculerMensualite(demande.Montant, tauxAnnuel, demande.DureeMois);

            // Mensualité assurance
            decimal cotisationAssurance = _calculAssurance.CalculMensualiteAssurance(demande.Profil, demande.Montant);

            // Tableau avec détails 
            var tableau = new List<LigneAmort>();
            decimal montantRestant = demande.Montant;

            // Cosntruction du tableau pour chaque mois
            for(int i = 1; i <= demande.DureeMois; i++)
            {
                decimal tauxMensuel = tauxAnnuel / 100m / 12m;
                decimal partInterets = montantRestant * tauxMensuel;
                decimal partCapital = mensualite - partInterets;
                montantRestant -= partCapital;

                tableau.Add(new LigneAmort
                {
                    Mois = i,
                    MensualiteTotale = mensualite,
                    Interets = partInterets,
                    PartPayee = partCapital,
                    ResteAPayer = montantRestant
                });

            }

            return new ResultatSimulation
            {
                PrixGlobalMensualite = mensualite,
                MensualiteAvecAssurance = cotisationAssurance,
                TotalInterets = tableau.Sum(l=>l.Interets),
                TotalAssurance = cotisationAssurance * demande.DureeMois,
                TableauAmortissement = tableau
            };

        }
    }
}
