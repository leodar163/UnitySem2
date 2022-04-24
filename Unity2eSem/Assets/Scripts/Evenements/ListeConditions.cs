using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvlleListeCondition", menuName = "Evénement/Liste de Conditions")]
    public class ListeConditions : ScriptableNarration
    {
        [SerializeField] private List<Condition> conditions = new List<Condition>();

        public List<Condition> Conditions => conditions;
        
        public static readonly string pathFichiers = "Assets/Narration/Conditions";
        
        public string[] RecupNomsConditions()
        {
            string[] noms = new string[conditions.Count];
            
            for (int i = 0; i < conditions.Count; i++)
            {
                noms[i] = conditions[i].nom;
            }

            return noms;
        }
        
        public string[] RecupNomsConditions(List<Condition> exclusions)
        {
            List<string> noms = new List<string>();
            
            foreach (var condition in conditions)
            {
                if (!exclusions.Contains(condition))
                {
                    noms.Add(condition.nom);
                }
            }

            return noms.ToArray();
        }

        public Condition RecupCondition(string nom)
        {
            return conditions.Find(condition => condition.nom == nom);
        }
        
        
        public void ReinitConditions()
        {
            foreach (var condition in conditions)
            {
                condition.estRemplie = false;
            }
        }
    }
    
    [Serializable]
    public class Condition
    {
        [SerializeField] [HideInInspector] public string nom = "nvlleCondition";
        public bool estRemplie;
        
        #if UNITY_EDITOR

        [HideInInspector] public string nomTemporaire = "";

        #endif

        public Condition(string nomCondition)
        {
            nom = nomCondition;
        }
    }
}
