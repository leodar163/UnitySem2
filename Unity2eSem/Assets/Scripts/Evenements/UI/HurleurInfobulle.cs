using UnityEngine;
using UnityEngine.EventSystems;

namespace Evenements.UI
{
    public class HurleurInfobulle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool estSurvole;
        [TextArea] public string messageAHurler = "";
        
        private void Update()
        {
            HurlerMessage();
        }
        
        private void HurlerMessage()
        {
            if (estSurvole && !string.IsNullOrEmpty(messageAHurler))
            {
                Infobulle.Singleton.AfficherInfobulle(messageAHurler);
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            estSurvole = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            estSurvole = false;
        }
    }
}