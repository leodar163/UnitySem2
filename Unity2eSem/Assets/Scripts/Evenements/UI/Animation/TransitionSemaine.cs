using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Evenements.UI.Animation
{
    public class TransitionSemaine : MonoBehaviour
    {
        private static TransitionSemaine cela;

        public static TransitionSemaine Singleton
        {
            get
            {
                if (!cela) cela = FindObjectOfType<TransitionSemaine>(true);
                if (!cela)
                    throw new NullReferenceException("Il n'y a pas d'animation Transition semaine dans l'interface");
                return cela;
            }
        }
        
        private Graphic[] graphics;
        [SerializeField] private TextMeshProUGUI titre;
        [SerializeField] private float tempsTransition = 2;
        [SerializeField] private float tempsFondu = 1;
        private bool animationEnCours;

        private void Awake()
        {
            graphics = GetComponentsInChildren<Graphic>();
        }

        private void Start()
        {
            
        }

        public UnityEvent TransitionnerSemaine(int numSemaine)
        {
            if(animationEnCours) return null;
            
            gameObject.SetActive(true);
            titre.text = "Semaine " + numSemaine;
            UnityEvent quandFini = new UnityEvent();
            StartCoroutine(AnimerTransition(quandFini));
            return quandFini;
        }

        private IEnumerator AnimerTransition(UnityEvent quandFini)
        {
            animationEnCours = true;
            FondreGraphics(0,0);
            FondreGraphics(1, tempsFondu);

            yield return new WaitForSeconds(tempsFondu + tempsTransition);
            
            FondreGraphics(0,tempsFondu);
            yield return new WaitForSeconds(tempsFondu);
            gameObject.SetActive(false);
            animationEnCours = false;
            quandFini.Invoke();
        }

        private void FondreGraphics(float alpha, float duree)
        {
            foreach (var graphic in graphics)
            {
                graphic.CrossFadeAlpha(alpha,duree, false);
            }
        }
    }
}