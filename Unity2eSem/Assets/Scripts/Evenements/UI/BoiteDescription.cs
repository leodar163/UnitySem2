using System;
using TMPro;
using UnityEngine;

namespace Evenements.UI
{
    public class BoiteDescription : MonoBehaviour
    {
        private static BoiteDescription cela;

        public static BoiteDescription Singleton
        {
            get
            {
                if (!cela) cela = FindObjectOfType<BoiteDescription>();
                if (!cela)
                    throw new NullReferenceException("Il manque une boite de description dans l'interface de la scene");
                return cela;
            }
        }
        
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI numSemaine;

        [HideInInspector] public string descriptionPersistente;
        [HideInInspector] public string descriptionEphemere;
        
        private void Update()
        {
            description.text = descriptionPersistente;
        }

        private void LateUpdate()
        {
            if (!string.IsNullOrEmpty(descriptionEphemere))
            {
                description.text = descriptionEphemere;
            }

            descriptionEphemere = null;
        }

        public void SetNumeroSemaine(int numeroSemaine)
        {
            numSemaine.text = "Semaine " + numeroSemaine;
        }
    }
}