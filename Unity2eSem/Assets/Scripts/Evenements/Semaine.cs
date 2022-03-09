using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvlleSemaine", menuName = "Evénement/Semaine")]
    public class Semaine : ScriptableNarration
    {
        [SerializeField] private List<Evenement> evenementsDepart;
        public List<Evenement> EvenementsDepart => evenementsDepart;
        
    }
}