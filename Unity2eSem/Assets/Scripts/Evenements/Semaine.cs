using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvlleSemaine", menuName = "Evénement/Semaine")]
    public class Semaine : ScriptableNarration
    {
        [SerializeField] private List<Evenement> evenementsDepart = new List<Evenement>();
        public List<Evenement> EvenementsDepart => evenementsDepart.FindAll(evenement => evenement);
        [SerializeField] public string description = "";
        
        [Serializable]
        public class Description
        {
            [SerializeField] public List<Condition> conditions = new List<Condition>();
            [SerializeField] public string description = "";

            public Description()
            {
                description = "";
                conditions = new List<Condition>();
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