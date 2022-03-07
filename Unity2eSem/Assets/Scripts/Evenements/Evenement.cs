using System.Collections.Generic;
using Plan;
using UnityEngine;

namespace Evenements
{
    public class Evenement : ScriptableObject
    {
        public Lieu lieu;
        public Sprite imageOverride;
        public string intro;

        public string description;

        private List<Choix> choix;
    }
}
