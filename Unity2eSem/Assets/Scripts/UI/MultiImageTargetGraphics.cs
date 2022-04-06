using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MultiImageTargetGraphics : MonoBehaviour
    {
        [SerializeField] private Graphic[] targetGraphics;
 
        public Graphic[] GetTargetGraphics => targetGraphics;
    }
}