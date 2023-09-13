using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleDefence
{
    [RequireComponent(typeof(Animator))]
    public class UnitAnimations : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] Unit unit;
        //[SerializeField] GameManager gameManager;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            unit.OnIdleAnimation += UnitAnimations_OnIdle;
            unit.OnRunAnimation += UnitAnimations_OnRun;
            unit.OnAttackAnimation += UnitAnimations_OnAttack;
            unit.OnDeathAnimation += UnitAnimations_OnDeath;
            unit.OnDanceAnimation += UnitAnimations_OnDance;
        }

        public void UnitAnimations_OnIdle(object sender, EventArgs e)
        {
            animator.SetBool("IsIdle", true);
        }

        public void UnitAnimations_OnRun(object sender, EventArgs e)
        {
            animator.SetBool("IsRun", true);
            animator.SetBool("IsAttack", false);
        }

        public void UnitAnimations_OnAttack(object sender, EventArgs e)
        {
            animator.SetBool("IsAttack", true);
            animator.SetBool("IsRun", false);
 
        }

        public void UnitAnimations_OnDeath(object sender, EventArgs e)
        {
            animator.SetTrigger("IsDeath");
            //animator.SetBool("IsDance", false);
        }

        public void UnitAnimations_OnDance(object sender, EventArgs e)
        {
            animator.SetTrigger("IsDance");
           // animator.SetBool("IsDeath", false);
        }
    }
}
