using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LieuxSettings : MonoBehaviour
{
    public bool debug = true;

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
    
    [Serializable]
    public struct Lieu
    {
        public string nom;
        public Vector2 position;
        public Sprite image;
    }

    [SerializeField] private List<Lieu> lieux = new List<Lieu>();
    public List<Lieu> Lieux => lieux;

    private void OnDrawGizmos()
    {
        if(debug)
        { 
            foreach (var lieu in lieux)
            {
                Gizmos.color = Color.green;
                Vector2 decalage= new Vector2(0.15f, 0.2f);
                Gizmos.DrawSphere(lieu.position,0.15f);

                Handles.Label(lieu.position + decalage,lieu.nom);
            }
        }
    }
}
