using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ressource
{
    public class RessourcesUI : MonoBehaviour
    {
        [SerializeField] private Slider barreSanteMentale;
        [SerializeField] private Slider barreEtude;
        [SerializeField] private TextMeshProUGUI quantiteArgent;

        private void Start()
        {
            Ressources.Singleton.quandEtudeChange.AddListener(FeedBackADefinir);
            Ressources.Singleton.quandSanteMentaleChange.AddListener(FeedBackADefinir);
            Ressources.Singleton.quandArgentChange.AddListener(FeedBackADefinir);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                barreEtude.value += 10;
            }
            
            MAJUIRessources();
        }

        
        
        private void MAJUIRessources()
        {
            barreEtude.value = Ressources.Singleton.Etude;
            barreSanteMentale.value = Ressources.Singleton.SanteMentale;
            quantiteArgent.text = Ressources.Singleton.Argent.ToString();
        }

        private void FeedBackADefinir(int diff)
        {
            
        }
    }
}