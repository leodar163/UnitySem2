using System.Collections.Generic;
using Evenements;
using UnityEngine;
using UnityEngine.UI;

namespace Plan
{
    public class Plan : MonoBehaviour
    {
        private static Plan cela;

        public static Plan Instance
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
        private readonly List<PinsLieu> pins = new List<PinsLieu>();
        
        private Semaine semaine;
        
        public void ChargerSemaine(Semaine semaineACharger)
        {
            semaine = semaineACharger;
            ChargerSemaine();
        }

        public void ChargerSemaine()
        {
            if (!semaine)
            {
                Debug.LogError("Tu charges une semaine sans en avoir assigner une");
                return;
            }
            
            NettoyerPins();
            
            foreach (var evenement in semaine.EvenementsDepart)
            {
                Lieu lieu = evenement.lieu;
                if (Instantiate(pinsBase.gameObject, lieu.Position, new Quaternion(), transform)
                    .TryGetComponent(out PinsLieu nvPins))
                {
                    nvPins.AssignerEvenement(evenement);
                }
            }
        }

        private void NettoyerPins()
        {
            for (int i = 0; i < pins.Count; i++)
            {
                Destroy(pins[i].gameObject);
            }
            pins.Clear();
        }
    }
}