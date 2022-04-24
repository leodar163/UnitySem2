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
        
        #if UNITY_EDITOR
        public bool retired;
        #endif
        
        public string[] RecupNomsConditions()
        {
            string[] noms = new string[conditions.Count];
            
            for (int i = 0; i < conditions.Count; i++)
            {
                noms[i] = conditions[i].nom;
            }

            return noms;
        }

        public string[] ParserNomConditions(List<Condition> conditionsAParser)
        {
            string[] noms = new string[conditionsAParser.Count];
            
            for (int i = 0; i < conditionsAParser.Count; i++)
            {
                noms[i] = conditionsAParser[i].nom;
            }

            return noms;
        }
        
        public string[] RecupNomsConditionsExlusif(List<Condition> exclusions)
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

        public Condition[] RecupConditions(string[] noms)
        {
            Condition[] conditionsRetour = new Condition[noms.Length];

            for (int i = 0; i < noms.Length; i++)
            {
                conditionsRetour[i] = RecupCondition(noms[i]);
            }

            return conditionsRetour;
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

        private bool remplie;
        
        public bool estRemplie
        {
            get => remplie;
            set
            {
                Debug.Log(value);
                remplie = value;
            }
        }
        
        #if UNITY_EDITOR

        [HideInInspector] public string nomTemporaire = "";

        #endif

        public Condition(string nomCondition)
        {
            Debug.Log("construite");
            nom = nomCondition;
        }
    }
}
