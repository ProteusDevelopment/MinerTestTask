using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using Player;

namespace Market
{
    public class MarketTrigger : MonoBehaviour
    {
        // Key - resource, value - rate.
        [SerializeField] private UnityKeyValuePair<string, int>[] _resourcesExchangesPairs;

        private readonly Dictionary<string, int> _resourcesExchanges = new Dictionary<string, int>();

        private void Awake()
        {
            foreach (var resourceExchangePair in _resourcesExchangesPairs)
            {
                _resourcesExchanges[resourceExchangePair.Key] = resourceExchangePair.Value;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerContainer player))
            {
                var playerResourcesToSell = new List<KeyValuePair<string, int>>();

                foreach (var resource in player.PlayerResources.Resources)
                {
                    foreach (var resourceExchange in _resourcesExchanges)
                    {
                        if (resource.Key == resourceExchange.Key)
                            playerResourcesToSell.Add(resourceExchange);
                    }
                }

                foreach (var resource in playerResourcesToSell)
                {
                    player.PlayerResources.SellResource(resource.Key, resource.Value);
                }
            }
        }
    }
}
