﻿using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using Player;
using UnityEngine;

namespace UI
{
    public class InventoryManager: MonoBehaviour
    {
        #region SingleTon
        /// <summary>
        /// Gets the sole instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static InventoryManager Instance { get; private set; }

        /// <summary>
        /// Awakes this instance (if none exist already).
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        #endregion
        
        public List<GameObject> inventoryItemPrefabs;

        public void AddItem(GameObject environmentItem)
        {
            EnvironmentItem clickedItem = environmentItem.GetComponent<EnvironmentItem>();

            GameObject correctSlot = clickedItem.stackable ? GetExistingItemSlot(clickedItem.item) : GetFirstFreeSlot();

            if (correctSlot == null)
            {
                PlayerHUD.Instance.AddMessage("You don't have room in your inventory.");
            }
            
            GameObject toSpawn = GetRightItem(clickedItem.item);
            if (toSpawn == null)
            {
                Debug.Log("You forgot the prefab...");
            }
            Instantiate(toSpawn, correctSlot.transform.position, Quaternion.identity, correctSlot.transform);
            Destroy(environmentItem);
        }
        
        private GameObject GetFirstFreeSlot()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).childCount == 0)
                {
                    return transform.GetChild(i).gameObject;
                }
            }
            return null;
        }
        
        private GameObject GetExistingItemSlot(ItemType itemType)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.childCount > 0 && child.gameObject.GetComponent<InventoryItem>().item == itemType)
                {
                    return child.gameObject;
                }
            }
            return GetFirstFreeSlot();
        }
        
        /// <summary>
        /// Get the right prefab to instantiate in the map
        /// </summary>
        /// <param name="itemType">The type of the item that was dropped</param>
        /// <returns></returns>
        private GameObject GetRightItem(ItemType itemType)
        {
            foreach (GameObject prefab in inventoryItemPrefabs)
            {
                if (prefab.GetComponent<InventoryItem>().item == itemType)
                {
                    return prefab;
                }
            }
            return null;
        }
    }
}