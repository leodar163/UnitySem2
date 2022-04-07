using System;
using Evenements;
using Evenements.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Plan
{
    [RequireComponent(typeof(HurleurDescription))]
    public class PinsLieu : MonoBehaviour
    {
        [Header("Interface")]
        [SerializeField] private Image iconePins;
        [SerializeField] private Image iconeLieu;
        [SerializeField] private Button bouton;
        [Header("Narration")]
        [SerializeField] private Evenement evenement;
        [Header("Message")] 
        [SerializeField] private HurleurDescription hurleur;
        
        
        public void AssignerEvenement(Evenement evenementAAssigner)
        {
            evenement = evenementAAssigner;
            iconeLieu.sprite = evenement.lieu.icone;
            hurleur.messageAHurler = evenementAAssigner.intro;
        }

        public void AssignerLieu(Lieu lieu)
        {
            transform.position = lieu.PositionProjetee;
            iconeLieu.sprite = lieu.icone;
        }
        
        public void LancerEvenement()
        {
            if(!evenement) return;
            EvenementUI.Singleton.ChargerEvenement(evenement);
        }
    }
}