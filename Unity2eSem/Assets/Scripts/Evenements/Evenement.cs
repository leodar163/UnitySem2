using System.Collections.Generic;
using Plan;
using UnityEngine;

namespace Evenements
{
    [CreateAssetMenu(fileName = "NvlEvenement", menuName = "Evénement/Evenement")]
    public class Evenement : ScriptableObject
    {
        [SerializeField][HideInInspector] public Lieu lieu;
        [Space]
        [SerializeField] public Sprite imageOverride;
        [Space]
        [TextArea(2,4)][SerializeField] public string intro;

        [Space]
        [TextArea(20,1000)][SerializeField] public string description;

        [SerializeField][HideInInspector] private List<Choix> choix;
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
        
        
        public static readonly string pathFichiers = "Assets/Narration/Evenements";
        
        public void RetirerChoix(Choix choixARetirer)
        {
            if (!choix.Contains(choixARetirer)) return;
            choix.Remove(choixARetirer);
            Destroy(choixARetirer);
        }

        
    }
}
