using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Evenements.UI
{
    public class EvenementUI : MonoBehaviour
    {
        private static EvenementUI cela;

        public static EvenementUI Singleton
        {
            get
            {
                if (!cela) cela = FindObjectOfType<EvenementUI>(true);
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
        [SerializeField] private ChoixUI choixUIBase;
        private List<ChoixUI> listeChoix = new List<ChoixUI>();

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

            description.text = evenement.description;
            
            ChargerChoix();
            OuvrirFenetre(true);
        }
        
        private void ChargerChoix()
        {
            NettoyerChoix();
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
            if (Instantiate(choixUIBase, zoneChoix).TryGetComponent(out ChoixUI nvChoix))
            {
                nvChoix.ChargerChoix(choixARajouter);
                listeChoix.Add(nvChoix);
            }
        }

        private void NettoyerChoix()
        {
            foreach (var choix in listeChoix)
            {
                Destroy(choix.gameObject);
            }
            listeChoix.Clear();
        }

        public void OuvrirFenetre(bool ouvrir)
        {
            gameObject.SetActive(ouvrir);
        }
    }
}