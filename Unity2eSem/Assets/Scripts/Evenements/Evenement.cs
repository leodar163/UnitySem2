using System.Collections.Generic;
using System.Linq;
using Plan;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvlEvenement", menuName = "Evénement/Evenement")]
    public class Evenement : ScriptableNarration
    {
        [SerializeField] public Lieu lieu;
        
        [SerializeField] public Sprite imageOverride;

        [SerializeField] public string titre = "";
        
        [SerializeField] public string intro = "";

        
        [SerializeField] public string description = "";

        [SerializeField][HideInInspector] private List<Choix> choix = new List<Choix>();

        public List<Choix> listeChoix
        {
            get
            {
                choix = choix.FindAll(choix1 => choix1 != null);
                return choix;
            }
        }

        [SerializeField] public List<Condition> conditions = new List<Condition>();

        public bool estDebloqued
        {
            get
            {
                return conditions.All(condition => condition.estRemplie);
            }
        }

        #if UNITY_EDITOR

        private bool[] choixDeployes;

        public bool[] ChoixDeployes
        {
            get
            {
                if (choixDeployes == null || choixDeployes.Length != choix.Count)
                {
                    choixDeployes = new bool[choix.Count];
                }

                return choixDeployes;
            }
        }
        
        #endif
    }
}
