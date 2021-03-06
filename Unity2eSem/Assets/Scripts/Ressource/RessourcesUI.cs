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
        [SerializeField] private TextMeshProUGUI quantiteSanteMentale;
        [SerializeField] private TextMeshProUGUI quantiteEtude;

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
            quantiteEtude.text = Ressources.Singleton.Etude.ToString();
            barreSanteMentale.value = Ressources.Singleton.SanteMentale;
            quantiteSanteMentale.text = Ressources.Singleton.SanteMentale.ToString();
            quantiteArgent.text = Ressources.Singleton.Argent.ToString();
        }

        private void FeedBackADefinir(int diff)
        {
            
        }
    }
}