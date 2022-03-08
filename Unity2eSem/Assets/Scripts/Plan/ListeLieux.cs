using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Plan
{
    public class ListeLieux : MonoBehaviour
    {
        [Header("Debug")]
        public bool debug = true;
        [SerializeField] private Color couleurDebug;

        private static ListeLieux cela;
        public static ListeLieux Instance
        {
            get
            {
                if (!cela) cela = FindObjectOfType<ListeLieux>();
                if (!cela) cela = new GameObject("LieuxSettings").AddComponent<ListeLieux>();
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

