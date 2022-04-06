using System;
using Evenements.Interface;
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

        [SerializeField] private TextMeshProUGUI descriptionSemaine;
        [SerializeField] private TextMeshProUGUI numSemaine;

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
            EvenementInterface.Singleton.OuvrirFenetre(false);
            indexSemaine = 0;
            ChargerSemaine(semestre.Semaines[indexSemaine]);   
        }
        
        private void ChargerSemaine(Semaine semaineACharger)
        {
            descriptionSemaine.text = semaineACharger.Description;
            numSemaine.text = "semaine " + (indexSemaine + 1);
            Plan.Plan.Singleton.ChargerSemaine(semaineACharger);
        }
        
        public void PasserSemaineSuivante()
        {
            EvenementInterface.Singleton.OuvrirFenetre(false);
            indexSemaine++;
            if(semestre.Semaines.Count <= indexSemaine) return;
            ChargerSemaine(semestre.Semaines[indexSemaine]);
        }
    }
}
