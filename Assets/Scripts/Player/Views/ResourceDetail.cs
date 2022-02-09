using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Views
{
    public class ResourceDetail : MonoBehaviour
    {
        [SerializeField] private Text _resourceNameText;
        [SerializeField] private Text _resourceAmountText;

        public void SetResourceName(string resourceName)
        {
            _resourceNameText.text = resourceName;
        }

        public void SetResourceAmount(string resourceAmount)
        {
            _resourceAmountText.text = resourceAmount;
        }
    }
}
