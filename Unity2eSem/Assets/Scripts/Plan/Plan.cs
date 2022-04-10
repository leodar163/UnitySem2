using System;
using System.Collections.Generic;
using Evenements;
using UnityEngine;
using UnityEngine.UI;

namespace Plan
{
    public class Plan : MonoBehaviour
    {
        private static Plan cela;

        public static Plan Singleton
        {
            get
            {
                if (!cela) cela = FindObjectOfType<Plan>();
                if (!cela) cela = new GameObject("LieuxSettings").AddComponent<Plan>();
                return cela;
            }
        }
        
        
        [SerializeField] private Image imagePlan;

        [SerializeField] private PinsLieu pinsBase;
        [SerializeField] private List<PinsLieu> pins = new List<PinsLieu>();
        
        private Semaine semaine;
        
        #if UNITY_EDITOR
        [HideInInspector] public bool debugListeLieu;

        [HideInInspector] public Semaine semaineDebug;
        #endif

        private void Awake()
        {
            NettoyerPins();
        }

        public void ChargerSemaine(Semaine semaineACharger, bool debug = false)
        {
            semaine = semaineACharger;
            ChargerSemaine(debug);
        }

        public void ChargerSemaine(bool debug = false)
        {
            if (!semaine)
            {
                Debug.LogError("Tu charges une semaine sans en avoir assigner une");
                return;
            }
            
            NettoyerPins();

            foreach (var evenement in debug  
                ? semaine.EvenementsDepart.FindAll(evenement => evenement)
                : semaine.EvenementsDepart.FindAll(evenement => evenement).FindAll(evenement => evenement.estDebloqued))
            {
                AjouterPins(evenement);
            }
        }

        public void ChargerListeLieux(ListeLieux lieux)
        {
            NettoyerPins();

            foreach (var lieu in lieux.Lieux)
            {
                AjouterPins(lieu);
            }
        }

        private void AjouterPins(Lieu lieu)
        {
            if (Instantiate(pinsBase.gameObject, lieu.PositionProjetee, new Quaternion(), transform)
                .TryGetComponent(out PinsLieu nvPins))
            {
                nvPins.AssignerLieu(lieu);
                pins.Add(nvPins);
            }
        }
        
        private void AjouterPins(Evenement evenement)
        {
            if(!evenement || evenement.lieu == null) return;
            if (Instantiate(pinsBase.gameObject, evenement.lieu.PositionProjetee, new Quaternion(), transform)
                .TryGetComponent(out PinsLieu nvPins))
            {
                nvPins.AssignerEvenement(evenement);
                pins.Add(nvPins);
            }
        }
        
        public void NettoyerPins()
        {
            foreach (var pin in pins)
            {
                if (Application.isPlaying)
                {
                    Destroy(pin.gameObject);    
                }
                else
                {
                    DestroyImmediate(pin.gameObject);
                }
                
            }

            pins.Clear();
        }
    }
}