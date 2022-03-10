using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvSemestre", menuName = "Evénement/Semestre")]
    public class Semestre : ScriptableNarration
    {
        [SerializeField] private List<Semaine> semaines = new List<Semaine>();
        public List<Semaine> Semaines => semaines;

        [SerializeField] [HideInInspector] public ListeConditions conditions;
        
        #if UNITY_EDITOR
        private bool[] semainesDeployes;

        public bool[] SemainesDeployes
        {
            get
            {
                if (semainesDeployes == null || semainesDeployes.Length != semaines.Count)
                {
                    semainesDeployes = new bool[semaines.Count];
                }

                return semainesDeployes;
            }
        }

        [HideInInspector] public bool ConditionsDeployees;
        #endif
        
        public void NettoyerSemaines()
        {
            semaines = semaines.FindAll(evenement => evenement != null);
            semaines.Add(null);
        }
    }
}