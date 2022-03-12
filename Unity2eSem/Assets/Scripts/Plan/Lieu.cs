using System;
using UnityEngine;

namespace Plan
{
    [Serializable]
    public class Lieu
    {
        [SerializeField] public string nom;
        [SerializeField] public Vector2 position;
        public Vector2 PositionProjetee => position * 100;
        [SerializeField] public Sprite icone;
        [SerializeField] public Sprite illustration;

#if UNITY_EDITOR

        [HideInInspector] public string nomTemporaire = "";

#endif
        
        public Lieu(string nomNvLieu)
        {
            nom = nomNvLieu;
        }
    }
}