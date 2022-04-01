using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Evenements.Interface
{
    public class ChoixInterface : MonoBehaviour
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
            if (choix.Couts.argent > 0) Ressources.Singleton.Argent -= choix.Couts.argent;
            if(choix.Couts.etude > 0) Ressources.Singleton.Etude -= choix.Couts.etude;
            if(choix.Couts.santeMentale > 0) Ressources.Singleton.SanteMentale -= choix.Couts.santeMentale;
            if (choix.Gains.argent > 0) Ressources.Singleton.Argent += choix.Gains.argent;
            if(choix.Gains.etude > 0) Ressources.Singleton.Etude += choix.Gains.etude;
            if(choix.Gains.santeMentale > 0) Ressources.Singleton.SanteMentale += choix.Gains.santeMentale;

            if (choix.evenementSuivant)
            {
                EvenementInterface.Singleton.ChargerEvenement(choix.evenementSuivant);
            }
            else
            {
                TimeLine.Singleton.PasserSemaineSuivante();
            }
        }
    }
}