using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Evenements.Interface
{
    public class EvenementInterface : MonoBehaviour
    {
        private static EvenementInterface cela;

        public static EvenementInterface Singleton
        {
            get
            {
                if (!cela) cela = FindObjectOfType<EvenementInterface>();
                if (!cela) throw new Exception("Il manque un interface d'événement dans la scene");
                return cela;
            }
        }
        
        [SerializeField] private Image illustration;
        [SerializeField] private TextMeshProUGUI titre;
        [SerializeField] private TextMeshProUGUI lieu;
        [SerializeField] private TextMeshProUGUI description;
        [Header("Choix")]
        [SerializeField] private RectTransform zoneChoix;
        [SerializeField] private ChoixInterface choixInterfaceBase;
        private List<ChoixInterface> listeChoix = new List<ChoixInterface>();

        private Evenement evenement;

        public void ChargerEvenement(Evenement evenementACharger)
        {
            if (evenementACharger.imageOverride)
            {
                illustration.sprite = evenementACharger.imageOverride;
            }
            else
            {
                illustration.sprite = evenementACharger.lieu.illustration;
            }

            titre.text = evenementACharger.titre;

            lieu.text = evenementACharger.lieu.nom;

            description.text = evenementACharger.description;
            
            
        }
    }
}