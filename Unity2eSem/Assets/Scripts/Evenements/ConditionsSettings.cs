using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    public class ConditionsSettings : ScriptableObject
    {
        [Serializable]
        public class Condition
        {
            [SerializeField] private string nom = "nvlleCondition";
            public string Nom => nom;
            public bool estRemplie;
        }

        [SerializeField] private List<Condition> conditions = new List<Condition>();

        public List<Condition> Conditions => conditions;
    }
}
