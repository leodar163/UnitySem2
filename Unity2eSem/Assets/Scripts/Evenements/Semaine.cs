using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvlleSemaine", menuName = "Evénement/Semaine")]
    public class Semaine : ScriptableNarration
    {
        [SerializeField] private List<Evenement> evenementsDepart = new List<Evenement>();
        public List<Evenement> EvenementsDepart => evenementsDepart;
        
        public string description
        {
            get
            {
                foreach (var _description in descriptions)
                {
                    if (_description.estDebloqued) return _description.description;
                }

                return "Aucune description de semaine débloquée. Pense à rajouter une descirption \"par défaut\"";
            }
        }
        
        [Serializable]
        public class Description
        {
            [SerializeField] public List<Condition> conditions;
            [SerializeField] public string description;

            public Description()
            {
                description = "";
                conditions = new List<Condition>();
            }

            public bool estDebloqued
            {
                get
                {
                    return conditions.All(condition => 
                        TimeLine.Singleton.listeConditions.RecupCondition(condition.nom).estRemplie);
                }
            }
            
        }

        [SerializeField] public List<Description> descriptions = new List<Description>();

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