using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerResources : MonoBehaviour
    {
        public readonly Dictionary<string, int> Resources = new Dictionary<string, int>();

        public UnityEvent<string> OnResourceAdded = new UnityEvent<string>();
        public UnityEvent<string> OnResourceAmountChanged = new UnityEvent<string>();

        public void AddResource(string name, int amount)
        {
            if (Resources.ContainsKey(name))
            {
                Resources[name] += amount;
                OnResourceAmountChanged.Invoke(name);
            }
            else
            {
                Resources[name] = amount;
                OnResourceAdded.Invoke(name);
            }
        }
    }
}
