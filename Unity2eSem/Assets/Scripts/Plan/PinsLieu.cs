using System;
using Evenements;
using UnityEngine;
using UnityEngine.UI;

namespace Plan
{
    public class PinsLieu : MonoBehaviour
    {
        [Header("Interface")]
        [SerializeField] private Image iconePins;
        [SerializeField] private Image iconeLieu;
        [SerializeField] private Button bouton;
        [Header("Narration")] 
        [SerializeField] private Evenement evenement;

        public void AssignerEvenement(Evenement evenementAAssigner)
        {
            evenement = evenementAAssigner;
            iconeLieu.sprite = evenement.lieu.icone;
        }
        
        public void LancerEvenement()
        {
            
        }
    }
}