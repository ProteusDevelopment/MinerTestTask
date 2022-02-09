using System;
using Crystals;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private Player _player;
        
        private Crystal _attackTarget = null;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            _player.PlayerAnimation.OnAttackAnimationEnded.AddListener(DamageAttackTarget);
        }

        private void OnDisable()
        {
            _player.PlayerAnimation.OnAttackAnimationEnded.RemoveListener(DamageAttackTarget);
        }

        private void FixedUpdate()
        {
            if (_attackTarget)
                return;
            
            if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward), out var hit, 1))
            {
                if (hit.collider.name.Contains("root"))
                {
                    _attackTarget = hit.collider.GetComponent<Crystal>();
                    
                    _player.PlayerAnimation.SetAttack();
                }
            }
        }

        private void DamageAttackTarget()
        {
            _attackTarget.Damage(1);

            _attackTarget = null;
        }
    }
}
