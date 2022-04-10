using System;
using UnityEngine;

namespace Plan
{
    [Serializable]
    public class Lieu
    {
        [SerializeField] public string nom;
        [SerializeField] public Vector2 position;

        public Vector2 PositionProjetee
        {
            get
            {
                if (GameObject.FindGameObjectWithTag("MainCanvas").TryGetComponent(out Canvas mainCanvas))
                {
                    return position * mainCanvas.referencePixelsPerUnit * mainCanvas.scaleFactor;
                }

                throw new ArgumentNullException("Impossible de projeter la position du lieu : " + nom
                    + "\nraison : Il n'y a pas de canvas dans la scene actuelle avec le tag \"MainCanvas\"");
            }
        }
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