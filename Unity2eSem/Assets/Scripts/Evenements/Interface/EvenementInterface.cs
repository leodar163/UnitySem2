using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Evenements.Interface
{
    public class EvenementInterface : MonoBehaviour
    {
        [SerializeField] private Image illustration;
        [SerializeField] private TextMeshProUGUI Titre;
        [SerializeField] private TextMeshProUGUI Description;
        [Header("Choix")]
        [SerializeField] private RectTransform zoneChoix;
        [SerializeField] private ChoixInterface choixInterfaceBase;
        private List<ChoixInterface> listeChoix = new List<ChoixInterface>();
        
        
    }
}