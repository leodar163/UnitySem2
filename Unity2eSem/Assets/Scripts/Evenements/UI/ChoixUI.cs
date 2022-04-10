using System;
using Ressource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Evenements.UI
{
    [RequireComponent(typeof(HurleurInfobulle))]
    public class ChoixUI : MonoBehaviour
    {
        private Choix choix;
        [SerializeField] private TextMeshProUGUI titre;
        [SerializeField] private Button bouton;
        [SerializeField] private HurleurInfobulle hurleur;

        private void OnValidate()
        {
            if (!hurleur && TryGetComponent(out hurleur))
            {
                
            }
        }

        private void Start()
        {
            bouton.onClick.AddListener(ActiverChoix);
            hurleur.messageAHurler = GenererMessageInfobulle();
        }

        private string GenererMessageInfobulle()
        {
            Color couleurRessourcesInsuffisantes = new Color(1, 0, 0.1f);
            string message = "";
            
            
            if (choix.Couts.argent > 0)
            {
                bool aPasSuffisammentArgent = Ressources.Singleton.Argent < choix.Couts.argent;
                message += (aPasSuffisammentArgent ? "<color=#" 
                                                  + ColorUtility.ToHtmlStringRGBA(couleurRessourcesInsuffisantes) + '>'
                    : "") + "-" + choix.Couts.argent + " argent\n" + (aPasSuffisammentArgent ? "</color>" : "");
            }
            if (choix.Couts.etude > 0)          
            {
                bool aPasSuffisammentEtude = Ressources.Singleton.Etude < choix.Couts.etude;
                message += (aPasSuffisammentEtude ? "<color=#" 
                                                  + ColorUtility.ToHtmlStringRGBA(couleurRessourcesInsuffisantes) + '>'
                    : "") + "-" + choix.Couts.etude + " étude\n" + (aPasSuffisammentEtude ? "</color>" : "");
            }
            if (choix.Couts.santeMentale > 0)   
            {
                bool aPasSuffisammentSanteMentale = Ressources.Singleton.SanteMentale < choix.Couts.santeMentale;
                message += (aPasSuffisammentSanteMentale ? "<color=#" 
                                                  + ColorUtility.ToHtmlStringRGBA(couleurRessourcesInsuffisantes) + '>'
                    : "") + "-" + choix.Couts.santeMentale + " santé mentale\n" + (aPasSuffisammentSanteMentale ? "</color>" : "");
            }
            
            if (choix.Gains.argent > 0)         message += "+" + choix.Gains.argent + " argent\n";
            if (choix.Gains.etude > 0)          message += "+" + choix.Gains.etude + " étude\n";
            if (choix.Gains.santeMentale > 0)   message += "+" + choix.Gains.santeMentale + " santé mentale\n";

            return message;
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