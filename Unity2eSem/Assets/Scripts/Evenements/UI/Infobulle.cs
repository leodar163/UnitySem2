using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Evenements.UI
{
    public class Infobulle : MonoBehaviour
    {
        private static Infobulle cela;

        public static Infobulle Singleton
        {
            get
            {
                if (!cela) cela = FindObjectOfType<Infobulle>();
                if (!cela) throw new NullReferenceException("Il n'y a pas d'infobulle dans l'interface");
                return cela;
            }
        }

        [SerializeField] private TextMeshProUGUI contenu;
        [SerializeField] private Graphic[] elementsGraphiques;
        [SerializeField] private RectTransform[] layouts;
        [SerializeField] private float tempsFondu = 1;
        [SerializeField] private float tempsAffichage = 0.5f;
        private IEnumerator coolDownAffichage;

        private void Start()
        {
            FondreGraphics(0, 0);
        }

        private void LateUpdate()
        {
            foreach (var rect in layouts)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            }
        }

        public void AfficherInfobulle(string contenuAAfficher)
        {
            FondreGraphics(1,0);
            
            if (coolDownAffichage != null)
            {
                StopCoroutine(coolDownAffichage);
            }

            coolDownAffichage = CoolDownAffichage();
            StartCoroutine(coolDownAffichage);
            
            contenu.text = contenuAAfficher;
        }
        
        private IEnumerator CoolDownAffichage()
        {
            float tmps = tempsAffichage;
            while (tmps > 0)
            {
                yield return new WaitForEndOfFrame();
                tmps -= Time.deltaTime;
            }
            FondreGraphics(0,tempsFondu);
        }
        
        private void FondreGraphics(float alphaCilbe, float secondes)
        {
            foreach (var graphic in elementsGraphiques)
            {
                graphic.CrossFadeAlpha(alphaCilbe,secondes,true);
            }
        }

    }
}