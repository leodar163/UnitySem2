using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionsSettings : MonoBehaviour
{

    [Serializable]
    public class Condition
    {
        [SerializeField] private string nom = "nvlleCondition";
        public string Nom => nom;
        public bool estRemplie;
    }

    [SerializeField] private List<Condition> conditions = new List<Condition>();

    public List<Condition> Conditions => conditions;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
