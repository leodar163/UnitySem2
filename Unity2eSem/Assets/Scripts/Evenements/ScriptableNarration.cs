using UnityEngine;

namespace Evenements
{
    public class ScriptableNarration : ScriptableObject
    {
        #if UNITY_EDITOR
        [HideInInspector] public string nomTemporaire = "";
        #endif
    }
}