using System.Collections.Generic;
using UnityEngine;
using Plan;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvlleListeLieu", menuName = "Narration/Liste de Lieux")]
    public class ListeLieux : ScriptableNarration
    {
        [SerializeField] private List<Lieu> lieux = new List<Lieu>();
        public List<Lieu> Lieux => lieux;
        
        public Lieu RecupLieu(string nom)
        {
            return lieux.Find(lieu => lieu.nom == nom);
        }

        public string[] RecupNomsLieux()
        {
            string[] nomsLieux = new string[lieux.Count];
            for (int i = 0; i < nomsLieux.Length; i++)
            {
                nomsLieux[i] = lieux[i].nom;
            }

            return nomsLieux;
        }
        
        public string[] RecupNomsLieux(List<Lieu> exclusions)
        {
            List<string> noms = new List<string>();
            
            foreach (var lieu in lieux)
            {
                if (!exclusions.Contains(lieu))
                {
                    noms.Add(lieu.nom);
                }
            }

            return noms.ToArray();
        }

        public Lieu RecupLieu(int index)
        {
            return index >= 0 && index < lieux.Count ? lieux[index] : null;
        }
    }
}