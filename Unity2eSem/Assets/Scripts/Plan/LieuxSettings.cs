using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Plan
{
    public class LieuxSettings : MonoBehaviour
    {
        [Header("Debug")]
        public bool debug = true;
        [SerializeField] private Color couleurDebug;

        private static LieuxSettings cela;
        public static LieuxSettings Instance
        {
            get
            {
                if (!cela) cela = FindObjectOfType<LieuxSettings>();
                if (!cela) cela = new GameObject("LieuxSettings").AddComponent<LieuxSettings>();
                return cela;
            }
        }
        
        [Header("Lieu")]
        [SerializeField] private List<Lieu> lieux = new List<Lieu>();
        public List<Lieu> Lieux => lieux;

        
        private void OnDrawGizmos()
        {
            if(debug)
            { 
                foreach (var lieu in lieux)
                {
                    Gizmos.color = couleurDebug;
                    Vector2 decalage= new Vector2(11f, 0.2f);
                    Gizmos.DrawSphere(lieu.Position,10f);

                    GUIStyle styleLabel = new GUIStyle
                    {
                        normal =
                        {
                            textColor = couleurDebug
                        },
                        fontSize = 18
                    };
                    Handles.Label(lieu.Position + decalage,lieu.nom, styleLabel);
                }
            }
        }

        public Lieu RecupererLieu(string nom)
        {
            return lieux.Find(lieu => lieu.nom == nom);
        }
    }
}

