using System;
using Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Items
{
    public abstract class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [Header("UI")] public Image image;

        public ItemType item;

        [HideInInspector] public Transform parentAfterDrag;

        private Canvas _myCanvas;
        

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_myCanvas == null)
            {
                _myCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
            }
            image.raycastTarget = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_myCanvas.transform as RectTransform, Input.mousePosition, _myCanvas.worldCamera, out pos);
            transform.position = _myCanvas.transform.TransformPoint(pos);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
            transform.localPosition.Set(transform.position.x, transform.position.y, 0);
        }
        
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                UseItem();
            }
        }

        public abstract void UseItem();

        public abstract void CombineItem(InventoryItem item);
    }

}