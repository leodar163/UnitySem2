using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvlleListeCondition", menuName = "Evénement/Liste de Conditions")]
    public class ListeConditions : ScriptableObject
    {
        

        [SerializeField] private List<Condition> conditions = new List<Condition>();

        public List<Condition> Conditions => conditions;
        
        public static readonly string pathFichiers = "Assets/Narration/Conditions";
    }
    
    [Serializable]
    public class Condition
    {
        [SerializeField] private string nom = "nvlleCondition";
        public string Nom => nom;
        public bool estRemplie;
    }
}
