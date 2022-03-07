using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    public class Semaine : ScriptableObject
    {
        [SerializeField] private List<Evenement> evenementsDepart;
        public List<Evenement> EvenementsDepart => evenementsDepart;
    }
}