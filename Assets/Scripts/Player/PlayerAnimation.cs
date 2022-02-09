using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationClip _attackClip;
        
        private Animator _animator;

        public UnityEvent OnAttackAnimationEnded = new UnityEvent();
        
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int AttackTrigger = Animator.StringToHash("attack");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetMoving(bool isMoving)
        {
            _animator.SetBool(IsMoving, isMoving);
        }

        public void SetAttack()
        {
            _animator.SetTrigger(AttackTrigger);
            
            StartCoroutine(CoroutinesHelper.WaitAndExecuteAction(_attackClip.length, OnAttackAnimationEnded.Invoke));
        }
    }
}
