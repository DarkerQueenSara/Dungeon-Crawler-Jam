using System;
using Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public abstract class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("UI")] public Image image;

        public ItemType item;

        [HideInInspector] public Transform parentAfterDrag;

        private void Start()
        {
            image = GetComponent<Image>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            image.raycastTarget = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
        }

        
        public void OnRightMouse(PointerEventData eventData)
        {
            if(Input.GetMouseButtonDown(1)){
                UseItem();
            }
        }

        public abstract void UseItem();

        public abstract bool CombineItem(InventoryItem item);
    }

}