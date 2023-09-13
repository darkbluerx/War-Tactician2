using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CastleDefence
{
    [RequireComponent(typeof(Animator))]
    public class SpectatorAnimations : MonoBehaviour
    {

        [SerializeField] Animator animator;

        [SerializeField] GameManager gameManager;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            gameManager.OnSittingAnimation += UnitAnimations_OnSitting;
            gameManager.OnAngryAnimation += UnitAnimations_OnAngry;
            gameManager.OnClappingAnimation += UnitAnimations_OnClapping;
            gameManager.OnRallyingAnimation += UnitAnimations_OnRallying;
            gameManager.OnCheeringAnimation += UnitAnimations_OnCheering;
        }

        public void UnitAnimations_OnSitting(object sender, EventArgs e)
        {
            animator.SetTrigger("IsSitting");
        }

        public void UnitAnimations_OnAngry(object sender, EventArgs e)
        {
            animator.SetTrigger("IsAngry");
        }
        public void UnitAnimations_OnClapping(object sender, EventArgs e)
        {
            animator.SetTrigger("IsClapping");
        }

        public void UnitAnimations_OnRallying(object sender, EventArgs e)
        {
            animator.SetTrigger("IsRallying");
        }

        public void UnitAnimations_OnCheering(object sender, EventArgs e)
        {
            animator.SetTrigger("IsCheering");
        }
    }
}
