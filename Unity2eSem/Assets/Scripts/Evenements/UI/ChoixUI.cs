using System;
using Ressource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Evenements.UI
{
    public class ChoixUI : MonoBehaviour
    {
        private Choix choix;
        [SerializeField] private TextMeshProUGUI titre;
        [SerializeField] private Button bouton;

        private void Start()
        {
            bouton.onClick.AddListener(ActiverChoix);
        }

        public void ActiverChoix()
        {
            if(!choix) return;
            
            if (choix.Couts.argent > 0)         Ressources.Singleton.Argent -= choix.Couts.argent;
            if (choix.Couts.etude > 0)          Ressources.Singleton.Etude -= choix.Couts.etude;
            if (choix.Couts.santeMentale > 0)   Ressources.Singleton.SanteMentale -= choix.Couts.santeMentale;
            
            if (choix.Gains.argent > 0)         Ressources.Singleton.Argent += choix.Gains.argent;
            if (choix.Gains.etude > 0)          Ressources.Singleton.Etude += choix.Gains.etude;
            if (choix.Gains.santeMentale > 0)   Ressources.Singleton.SanteMentale += choix.Gains.santeMentale;

            if (choix.Consequences != null)
            {
                foreach (var condition in choix.Consequences)
                {
                    condition.estRemplie = true;
                }
            }

            if (choix.evenementSuivant)
            {
                EvenementUI.Singleton.ChargerEvenement(choix.evenementSuivant);
            }
            else
            {
                TimeLine.Singleton.PasserSemaineSuivante();
            }
        }

        public void ChargerChoix(Choix choixACharger)
        {
            choix = choixACharger;
            ChargerChoix();
        }

        public void ChargerChoix()
        {
            titre.text = choix.Description;

            if (!choix.aAssezRessources)
            {
                titre.alpha = 0.5f;
                bouton.interactable = false;
            }
        }
    }
}