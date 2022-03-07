using System;
using UnityEngine;

namespace Plan
{
    [Serializable]
    public struct Lieu
    {
        public string nom;
        [SerializeField] private Vector2 position;
        public Vector2 Position => position * 100;
        public Sprite icone;
        public Sprite illustration;
    }
}