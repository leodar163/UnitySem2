using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plan
{
    public class LieuxManager : MonoBehaviour
    {
        [Header("Debug")]
        public bool debug = true;
        [SerializeField] private Color couleurDebug;

        private static LieuxManager cela;
        public static LieuxManager Instance
        {
            get
            {
                if (!cela) cela = FindObjectOfType<LieuxManager>();
                if (!cela) cela = new GameObject("LieuxSettings").AddComponent<LieuxManager>();
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
                    Gizmos.DrawSphere(lieu.PositionProjetee,10f);

                    GUIStyle styleLabel = new GUIStyle
                    {
                        normal =
                        {
                            textColor = couleurDebug
                        },
                        fontSize = 18
                    };
                    #if UNITY_EDITOR
                    UnityEditor.Handles.Label(lieu.PositionProjetee + decalage,lieu.nom, styleLabel);
                    #endif
                }
            }
        }

        public Lieu RecupLieu(string nom)
        {
            return lieux.Find(lieu => lieu.nom == nom);
        }

        public string[] RecupNomsLieux()
        {
            string[] nomsLieux = new string[lieux.Count];
            for (int i = 0; i < nomsLieux.Length; i++)
            {
                nomsLieux[i] = lieux[i].nom;
            }

            return nomsLieux;
        }
    }
}

