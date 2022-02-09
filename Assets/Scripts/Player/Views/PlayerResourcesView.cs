using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Views
{
    [RequireComponent(typeof(PlayerResources))]
    public class PlayerResourcesView : MonoBehaviour
    {
        [SerializeField] private Transform _resourceDetailLayout;

        private Dictionary<string, ResourceDetail> _resourceDetails = new Dictionary<string, ResourceDetail>();

        private PlayerResources _playerResources;

        private void Awake()
        {
            _playerResources = GetComponent<PlayerResources>();
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

        private void AddNewResource(string name)
        {
            _resourceDetails[name] = Instantiate(Resources.Load<ResourceDetail>("/Player/Views/ResourceDetail"), _resourceDetailLayout);

            _resourceDetails[name].SetResourceName(string.Format("{0}:", name));
            ChangeResourceAmount(name);
        }

        private void ChangeResourceAmount(string name)
        {
            _resourceDetails[name].SetResourceAmount(_playerResources.Resources[name].ToString());
        }
    }
}
