using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvlleSemaine", menuName = "Evénement/Semaine")]
    public class Semaine : ScriptableNarration
    {
        [SerializeField] private List<Evenement> evenementsDepart = new List<Evenement>();
        public List<Evenement> EvenementsDepart => evenementsDepart;
        
        #if UNITY_EDITOR
        
        private bool[] evenementsDeployes;

        public bool[] EvenementsDeployes
        {
            get
            {
                if (evenementsDeployes == null || evenementsDeployes.Length != evenementsDepart.Count)
                {
                    evenementsDeployes = new bool[evenementsDepart.Count];
                }

                return evenementsDeployes;
            }
        }
        #endif


        public void NettoyerEvenementsDepart()
        {
            evenementsDepart = evenementsDepart.FindAll(evenement => evenement != null);
            evenementsDepart.Add(null);
        }
    }
}