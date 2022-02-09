using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Crystals
{
    public class Crystal : Damagable
    {
        private const float RecoveringTime = 5f;
        
        [SerializeField] private string _resourceName;
        [SerializeField] private int _resourceValuePerDamage;
        
        // Key - health, Value - model.
        [SerializeField] private UnityKeyValuePair<int, GameObject>[] _models;

        private bool _isRecovering = false;
        private float _recoveringTimer = 0;

        private void Awake()
        {
            try
            {
                _health = _models[0].Key;
            }
            catch (IndexOutOfRangeException)
            {
                Debug.LogError("Models must not be empty");
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_isRecovering)
                return;

            if (other.CompareTag("Player"))
                ResetRecoveringTimer();
        }

        private void Update()
        {
            if (!_isRecovering)
                return;

            _recoveringTimer -= Time.deltaTime;

            if (_recoveringTimer < 0)
            {
                UpgradeModel();
            }
        }

        public override bool TryDamage(int amount, out KeyValuePair<string, int> takenResource)
        {
            if (_health == _models[_models.Length - 1].Key)
            {
                return false;
            }
            
            _health -= amount;
            DowngradeModel();

            if (_health <= _models[1].Key)
            {
                _isRecovering = true;
                ResetRecoveringTimer();
            }

            takenResource = new KeyValuePair<string, int>(_resourceName, _resourceValuePerDamage);
            return true;
        }

        private void DowngradeModel()
        {
            for (int i = 1; i < _models.Length; i++)
            {
                if (_health == _models[i].Key)
                {
                    _models[i - 1].Value.SetActive(false);
                    _models[i].Value.SetActive(true);
                    break;
                }
            }
        }

        private void UpgradeModel()
        {
            for (int i = _models.Length - 2; i >= 0; i--)
            {
                if (_health < _models[i].Key)
                {
                    _health = _models[i].Key;
                    _models[i].Value.SetActive(true);
                    _models[i + 1].Value.SetActive(false);

                    if (i == 0)
                    {
                        // Восстановили польностью.
                        _isRecovering = false;
                    }
                    else
                    {
                        ResetRecoveringTimer();
                    }

                    break;
                }
            }
        }

        private void ResetRecoveringTimer()
        {
            _recoveringTimer = RecoveringTime;
        }
    }
}
