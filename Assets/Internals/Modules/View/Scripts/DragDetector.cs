using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace BasicLegionInfected.View
{
    public class DragDetector : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public bool IsDragging = false;

        public void OnBeginDrag(PointerEventData eventData)
        {
            IsDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            IsDragging = false;
        }
    }
}
