using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressources : MonoBehaviour
{
    private static Ressources cela;

    public static Ressources Singleton
    {
        get
        {
            if (!cela) cela = FindObjectOfType<Ressources>();
            if (!cela) cela = new GameObject("Ressource").AddComponent<Ressources>();
            return cela;
        }
    }
    
    [SerializeField] private float etude;
    [SerializeField] private float santeMentale; 
    [SerializeField] private int argent;

    public float Etude
    {
        get => etude;
        set => etude = value;
    }

    public float SanteMentale
    {
        get => santeMentale;
        set => santeMentale = value;
    }

    public int Argent
    {
        get => argent;
        set => argent = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
