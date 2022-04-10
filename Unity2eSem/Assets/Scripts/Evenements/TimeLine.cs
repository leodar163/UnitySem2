using System;
using Evenements.UI;
using Evenements.UI.Animation;
using TMPro;
using UnityEngine;

namespace Evenements
{
    public class TimeLine : MonoBehaviour
    {
        private static TimeLine cela;

        public static TimeLine Singleton
        {
            get
            {
                if (!cela) cela = FindObjectOfType<TimeLine>();
                if (!cela) cela = new GameObject("TimeLine").AddComponent<TimeLine>();
                return cela;
            }
        }   
        
        [Tooltip("Semestre lancé au démarage du jeu")]
        [SerializeField] private Semestre semestre;

        private int indexSemaine;
        
        // Start is called before the first frame update
        void Start()
        {
            if(semestre) ChargerSemestre();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void ChargerSemestre(Semestre semestreACharger)
        {
            semestre = semestreACharger;
        }

        private void ChargerSemestre()
        {
            if (semestre.Semaines == null || semestre.Semaines.Count == 0)
                throw new NullReferenceException("Impossible de charger le semestre " + semestre.name +
                                                 ":\nLa liste de semaine est soit nulle, soit vide");
            EvenementUI.Singleton.OuvrirFenetre(false);
            indexSemaine = 0;
            ChargerSemaine(semestre.Semaines[indexSemaine]);   
        }
        
        private void ChargerSemaine(Semaine semaineACharger)
        {
            if(semaineACharger == null) return;
            BoiteDescription.Singleton.descriptionPersistente = semaineACharger.description;
            BoiteDescription.Singleton.SetNumeroSemaine(indexSemaine + 1);
            Plan.Plan.Singleton.ChargerSemaine(semaineACharger);
        }
        
        public void PasserSemaineSuivante()
        {
            EvenementUI.Singleton.OuvrirFenetre(false);
            indexSemaine++;
            if(semestre.Semaines.Count <= indexSemaine || semestre.Semaines[indexSemaine] == null) return;
            Plan.Plan.Singleton.NettoyerPins();
            TransitionSemaine.Singleton.TransitionnerSemaine(indexSemaine + 1).AddListener(() =>
            {
                ChargerSemaine(semestre.Semaines[indexSemaine]);
            });
        }
    }
}
