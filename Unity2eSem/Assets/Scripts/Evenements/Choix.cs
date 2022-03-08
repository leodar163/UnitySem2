using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvChoix", menuName = "Evénement/Choix")]
    public class Choix : ScriptableObject
    {
        [Serializable]
        public struct Gain
        {
            [Min(0)] public int argent;
            [Range(0, 100)] public float santeeMentale;
            [Range(0, 100)] public float etude;
        }

        [SerializeField][HideInInspector] private readonly List<Condition> conditions = new List<Condition>();
        [SerializeField][HideInInspector] private readonly List<Condition> consequences = new List<Condition>();

        public List<Condition> Conditions => conditions;
        public List<Condition> Consequences => consequences;

        [SerializeField] private Evenement evenementSuivant;
    
        [SerializeField] private Gain couts;
        [SerializeField] private Gain gains;

        public static readonly string pathFichiers = "Assets/Narration/Choix";
        
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
    }
}
