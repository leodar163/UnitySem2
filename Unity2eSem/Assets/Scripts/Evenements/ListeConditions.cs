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
        
        public string[] RecupNomsConditions()
        {
            string[] noms = new string[conditions.Count];
            
            for (int i = 0; i < conditions.Count; i++)
            {
                noms[i] = conditions[i].Nom;
            }

            return noms;
        }
        
        public string[] RecupNomsConditions(List<Condition> exculsions)
        {
            string[] noms = new string[conditions.Count - exculsions.Count];
            
            for (int i = 0; i < conditions.Count; i++)
            {
                if (!exculsions.Contains(conditions[i]))
                {
                    noms[i] = conditions[i].Nom;
                }
            }

            return noms;
        }

        public Condition RecupCondition(string nom)
        {
            return conditions.Find(condition => condition.Nom == nom);
        }
    }
    
    [Serializable]
    public class Condition
    {
        [SerializeField] private string nom = "nvlleCondition";
        public string Nom => nom;
        public bool estRemplie;
    }
    
    
}
