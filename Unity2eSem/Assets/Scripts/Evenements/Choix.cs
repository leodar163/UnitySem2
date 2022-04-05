using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvChoix", menuName = "Evénement/Choix")]
    public class Choix : ScriptableNarration
    {
        [Serializable]
        public struct Gain
        {
            [Min(0)] public int argent;
            [Range(0, 100)] public int santeMentale;
            [Range(0, 100)] public int etude;
        }

        [SerializeField] public string Description;
        
        [SerializeField][HideInInspector] private List<Condition> conditions = new List<Condition>();
        [SerializeField][HideInInspector] private List<Condition> consequences = new List<Condition>();
 
        public List<Condition> Conditions => conditions;
        public List<Condition> Consequences => consequences;

        #if UNITY_EDITOR
        [HideInInspector] public bool montrerEvenementSuivant; 
        #endif
        
        [SerializeField] public Evenement evenementSuivant;
    
        [SerializeField] private Gain couts;
        [SerializeField] private Gain gains;

        public Gain Couts
        {
            get => couts;
            set => couts = value;
        }

        public Gain Gains
        {
            get => gains;
            set => gains = value;
        }

        public void NettoyezConditions()
        {
            conditions = conditions.FindAll(condition => condition != null);
            consequences = consequences.FindAll(condition => condition != null);
        }

        public bool estDebloqued
        {
            get
            {
                return conditions.All(condition => condition.estRemplie);
            }
        }

        public bool aAssezRessources
        {
            get
            {
                return Ressources.Singleton.Argent >= couts.argent
                       && Ressources.Singleton.Etude >= couts.etude
                       && Ressources.Singleton.SanteMentale >= couts.santeMentale;
            }
        }
    }
}
