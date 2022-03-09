using System.Collections.Generic;
using Plan;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvlEvenement", menuName = "Evénement/Evenement")]
    public class Evenement : ScriptableNarration
    {
        [SerializeField] public Lieu lieu;
        
        [SerializeField] public Sprite imageOverride;
        
        [SerializeField] public string intro = "";

        
        [SerializeField] public string description = "";

        [SerializeField][HideInInspector] private List<Choix> choix = new List<Choix>();
        public List<Choix> listeChoix => choix; 

        #if UNITY_EDITOR
        private int focusChoix = -1;
        public int FocusChoix
        {
            get
            {
                if (focusChoix > choix.Count) focusChoix = -1;
                return focusChoix;
            }
            set => focusChoix = value;
            
        }
        #endif

        public void RetirerChoix(Choix choixARetirer)
        {
            if (!choix.Contains(choixARetirer)) return;
            choix.Remove(choixARetirer);
            Destroy(choixARetirer);
        }

        
    }
}
