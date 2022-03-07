using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    public class Choix : ScriptableObject
    {
        [Serializable]
        public struct Gain
        {
            [Min(0)] public int argent;
            [Range(0, 100)] public float santeeMentale;
            [Range(0, 100)] public float etude;
        }

        [SerializeField][HideInInspector] private readonly List<ConditionsSettings.Condition> conditions = new List<ConditionsSettings.Condition>();
        [SerializeField][HideInInspector] private readonly List<ConditionsSettings.Condition> consequences = new List<ConditionsSettings.Condition>();

        public List<ConditionsSettings.Condition> Conditions => conditions;
        public List<ConditionsSettings.Condition> Consequences => consequences;

        [SerializeField] private Evenement evenementSuivant;
    
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
    }
}
