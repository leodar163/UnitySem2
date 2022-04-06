using System;
using Evenements;
using Evenements.Interface;
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

        public void AssignerLieu(Lieu lieu)
        {
            transform.position = lieu.PositionProjetee;
            iconeLieu.sprite = lieu.icone;
        }
        
        public void LancerEvenement()
        {
            if(!evenement) return;
            EvenementInterface.Singleton.ChargerEvenement(evenement);
        }
    }
}