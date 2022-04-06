using UnityEngine;
using UnityEngine.Events;

namespace Ressource
{
    public class Ressources : MonoBehaviour
    {
        private static Ressources cela;

        public static Ressources Singleton
        {
            get
            {
                if (!cela) cela = FindObjectOfType<Ressources>();
                if (!cela) cela = new GameObject("Ressource").AddComponent<Ressources>();
                return cela;
            }
        }
    
        [HideInInspector] public UnityEvent<int> quandEtudeChange = new UnityEvent<int>();
        [HideInInspector] public UnityEvent<int> quandSanteMentaleChange = new UnityEvent<int>();
        [HideInInspector] public UnityEvent<int> quandArgentChange = new UnityEvent<int>();

        [SerializeField] private int etude;
        [SerializeField] private int santeMentale; 
        [SerializeField] private int argent;

        public int Etude
        {
            get => etude;
            set
            {
                int nvlleValeur = Mathf.Clamp(value, 0, 100);
                quandEtudeChange.Invoke(nvlleValeur - etude);
                etude = nvlleValeur;
            }
        }

        public int SanteMentale
        {
            get => santeMentale;
            set
            {
                int nvlleValeur = Mathf.Clamp(value, 0, 100);
                quandSanteMentaleChange.Invoke(nvlleValeur - santeMentale);
                santeMentale = nvlleValeur;
            }
        }

        public int Argent
        {
            get => argent;
            set
            {
                //Pour le moment il se passe rien quand tu a des dettes
                quandEtudeChange.Invoke(value - argent);
                argent = value;
            }
        }

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
