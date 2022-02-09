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
        private Animator _animator;

        public UnityEvent OnAttackAnimationEnded = new UnityEvent();

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetMoving(bool isMoving)
        {
            _animator.SetBool("isMoving", isMoving);
        }

        public void SetAttack()
        {
            _animator.SetTrigger("attack");

            AnimatorStateInfo stateInfo = _animator.GetNextAnimatorStateInfo(0);
            if (stateInfo.IsName("NormalAttack01_SwordShield"))
            {
                Debug.Log("Attack state");
                StartCoroutine(CoroutinesHelper.WaitAndExecuteAction(stateInfo.length, () =>
                {
                    Debug.Log("ended");
                    OnAttackAnimationEnded.Invoke();
                }));
            }
        }
    }
}
