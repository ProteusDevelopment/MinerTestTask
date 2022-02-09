using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerResources : MonoBehaviour
    {
        public const string GoldResourceName = "Gold";

        private static readonly string[] CrystalsResourcesNames = new string[] {
            "Red", "Green", "Blue"
        };

        public readonly Dictionary<string, int> Resources = new Dictionary<string, int>();

        private int _inventorySize = 30;

        public UnityEvent<string> OnResourceAdded = new UnityEvent<string>();
        public UnityEvent<string> OnResourceAmountChanged = new UnityEvent<string>();

        public bool IsInventoryFull => GetCrystalsAmount() >= _inventorySize;

        public void SetExistResource(string resourceName, int amount)
        {
            Resources[resourceName] = amount;
            OnResourceAmountChanged.Invoke(resourceName);
        }
        
        public void AddResource(string resourceName, int amount)
        {
            if (Resources.ContainsKey(resourceName))
            {
                Resources[resourceName] += amount;
                OnResourceAmountChanged.Invoke(resourceName);
            }
            else
            {
                Resources[resourceName] = amount;
                OnResourceAdded.Invoke(resourceName);
            }
        }

        public void SubResource(string resourceName, int amount)
        {
            AddResource(resourceName, -amount);
        }

        public int GetCrystalsAmount()
        {
            return Resources
                .Where(resource => CrystalsResourcesNames.Any(str => str == resource.Key))
                .Sum(resource => resource.Value);
        }

        public void SellResource(string resourceName, int exchangeRate)
        {
            if (!Resources.ContainsKey(resourceName))
            {
                return;
            }

            AddResource(GoldResourceName, Resources[resourceName] * exchangeRate);
            SetExistResource(resourceName,0);
        }
        
        public void UpgradeInventory()
        {
            if (Resources[GoldResourceName] < 50)
                return;
            
            _inventorySize += 10;
            SubResource(GoldResourceName, 50);
        }
    }
}
