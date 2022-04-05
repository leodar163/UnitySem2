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

        [SerializeField] private Evenement evenementTest;
        [Space]
        [SerializeField] private Image illustration;
        [SerializeField] private TextMeshProUGUI titre;
        [SerializeField] private TextMeshProUGUI lieu;
        [SerializeField] private TextMeshProUGUI description;
        [Header("Choix")]
        [SerializeField] private RectTransform zoneChoix;
        [SerializeField] private ChoixInterface choixInterfaceBase;
        private List<ChoixInterface> listeChoix = new List<ChoixInterface>();

        private Evenement evenement;

        private void Start()
        {
            if(evenementTest) ChargerEvenement(evenementTest);
        }

        public void ChargerEvenement(Evenement evenementACharger)
        {
            evenement = evenementACharger;
            
            ChargerEvenement();
        }

        public void ChargerEvenement()
        {
            if (!evenement) throw new NullReferenceException("La propriété \"evenement\" n'est pas assignée");
            
            if (evenement.imageOverride)
            {
                illustration.sprite = evenement.imageOverride;
            }
            else
            {
                illustration.sprite = evenement.lieu.illustration;
            }

            titre.text = evenement.titre;
            //evenement.lieu.nom = "Bar";
            lieu.text = evenement.lieu.nom;
            
            print(evenement.lieu.nom);

            description.text = evenement.description;
            
            ChargerChoix();
        }
        
        private void ChargerChoix()
        {
            foreach (var choix in evenement.listeChoix)
            {
                if (choix != null && choix.estDebloqued)
                {
                  AjouterChoix(choix);   
                }
            }
        }

        private void AjouterChoix(Choix choixARajouter)
        {
            if(Instantiate(choixInterfaceBase, zoneChoix).TryGetComponent(out ChoixInterface nvChoix))
            {
                nvChoix.ChargerChoix(choixARajouter);
            }
        }
    }
}