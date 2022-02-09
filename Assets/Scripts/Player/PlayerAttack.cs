using System;
using System.Linq;
using Crystals;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerContainer _player;
        
        private bool _canAttack = true;
        private Damagable _attackTarget = null;

        private void Awake()
        {
            _player = GetComponent<PlayerContainer>();
        }

        private void OnEnable()
        {
            _player.PlayerAnimation.OnAttackAnimationEnded.AddListener(DamageAttackTarget);
            
            _player.PlayerResources.OnResourceAmountChanged.AddListener(CheckAttackAbility);
        }

        private void OnDisable()
        {
            _player.PlayerAnimation.OnAttackAnimationEnded.RemoveListener(DamageAttackTarget);
            
            _player.PlayerResources.OnResourceAmountChanged.RemoveListener(CheckAttackAbility);
        }

        private void FixedUpdate()
        {
            if (_attackTarget || !_canAttack)
                return;
            
            if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward), out var hit, 1))
            {
                if (hit.collider.isTrigger)
                    return;
                
                if (hit.collider.TryGetComponent(out _attackTarget))
                {
                    _player.PlayerAnimation.SetAttack();
                }
            }
        }

        private void DamageAttackTarget()
        {
            if (_attackTarget.TryDamage(1, out var resource))
            {
                _player.PlayerResources.AddResource(resource.Key, resource.Value);
            }

            _attackTarget = null;
        }

        private void CheckAttackAbility(string resourceName)
        {
            _canAttack = !_player.PlayerResources.IsInventoryFull;
        }
    }
}
