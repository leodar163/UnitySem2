using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evenement : ScriptableObject
{
    public LieuxSettings.Lieu lieu;

    public string intro;

    public string description;

    private List<Choix> choix;
}
