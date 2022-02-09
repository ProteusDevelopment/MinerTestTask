using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Views
{
    [RequireComponent(typeof(PlayerResources))]
    public class PlayerResourcesView : MonoBehaviour
    {
        private const string ResourceDetailPath = "Player/Views/ResourceDetail";
        
        [SerializeField] private Transform _resourceDetailLayout;

        private Dictionary<string, ResourceDetail> _resourceDetails = new Dictionary<string, ResourceDetail>();

        private PlayerResources _playerResources;

        private void Awake()
        {
            _playerResources = GetComponent<PlayerResources>();
            
            AddNewResource(PlayerResources.GoldResourceName);
        }

        private void OnEnable()
        {
            _playerResources.OnResourceAdded.AddListener(AddNewResource);
            _playerResources.OnResourceAmountChanged.AddListener(ChangeResourceAmount);
        }

        private void OnDisable()
        {
            _playerResources.OnResourceAdded.RemoveListener(AddNewResource);
            _playerResources.OnResourceAmountChanged.RemoveListener(ChangeResourceAmount);
        }

        private void AddNewResource(string resourceName)
        {
            if (!_playerResources.Resources.ContainsKey(resourceName))
                _playerResources.Resources[resourceName] = 0;
            
            _resourceDetails[resourceName] =
                Instantiate(Resources.Load<ResourceDetail>(ResourceDetailPath),
                    _resourceDetailLayout);

            _resourceDetails[resourceName].SetResourceName($"{resourceName}:");
            ChangeResourceAmount(resourceName);
        }

        private void ChangeResourceAmount(string resourceName)
        {
            _resourceDetails[resourceName].SetResourceAmount(_playerResources.Resources[resourceName].ToString());
        }
    }
}
