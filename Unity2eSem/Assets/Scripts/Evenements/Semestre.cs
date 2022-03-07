using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    public class Semestre : ScriptableObject
    {
        [SerializeField] private List<Semaine> semaines = new List<Semaine>();
        public List<Semaine> Semaines => semaines;
    }
}