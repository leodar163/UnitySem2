using System.Collections.Generic;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvSemestre", menuName = "Evénement/Semestre")]
    public class Semestre : ScriptableObject
    {
        [SerializeField] private List<Semaine> semaines = new List<Semaine>();
        public List<Semaine> Semaines => semaines;
        
        public static readonly string pathFichiers = "Assets/Narration/Semestres";
    }
}