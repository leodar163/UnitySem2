using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvSemestre", menuName = "Evénement/Semestre")]
    public class Semestre : ScriptableNarration
    {
        [SerializeField] private List<Semaine> semaines = new List<Semaine>();
        public List<Semaine> Semaines => semaines.FindAll(semaine => semaine != null);

        [SerializeField] [HideInInspector] public ListeConditions conditions;
        [SerializeField] [HideInInspector] public ListeLieux lieux;
        
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

        [HideInInspector] public bool LieuxDeployees;
        #endif
        
        public void NettoyerSemaines()
        {
            semaines = semaines.FindAll(evenement => evenement != null);
            semaines.Add(null);
        }
    }
}