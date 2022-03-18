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
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
