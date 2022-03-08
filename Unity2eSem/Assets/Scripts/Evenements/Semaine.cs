using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvlleSemaine", menuName = "Evénement/Semaine")]
    public class Semaine : ScriptableObject
    {
        [SerializeField] private List<Evenement> evenementsDepart;
        public List<Evenement> EvenementsDepart => evenementsDepart;
        
        public static readonly string pathFichiers = "Assets/Narration/Semaines";
    }
}