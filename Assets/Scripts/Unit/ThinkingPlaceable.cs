using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CastleDefence
{
    public class ThinkingPlaceable : Placeable
    {
        [HideInInspector] public States state = States.Seeking;
        public enum States
        {
            Idle, //at the very beginning, when dropped
            Seeking, //going for the target
            Attacking, //attack cycle animation, not moving
            Dead, //dead animation, before removal from play field
        }

        [HideInInspector] public AttackType attackType;
        public enum AttackType
        {
            Melee,
            Ranged,
        }

        [HideInInspector] public ThinkingPlaceable target;
        [HideInInspector] public HealthBar healthBar;

        [HideInInspector] public float hitPoints;
        [HideInInspector] public float attackRange;
        [HideInInspector] public float attackRatio;
        [HideInInspector] public float lastBlowTime = -1000f;
        [HideInInspector] public float damage;

        [HideInInspector] public float timeToActNext = 0f;

		//Inspector references
		[Header("Projectile for Ranged")]
		public GameObject projectilePrefab;
		public Transform projectileSpawnPoint;

		//private Projectile projectile;
		protected AudioSource audioSource;

		public UnityAction<ThinkingPlaceable> OnDealDamage;

        public virtual void SetTarget(ThinkingPlaceable t)
        {
            if (t != null) 
           target = t;

          if(t != null) t.OnDie += TargetIsDead;
            
            //else target = null;
        }

        public virtual void StartAttack()
        {
            state = States.Attacking;
        }

        public virtual void DealBlow()
        {
            lastBlowTime = Time.time;   
        }

		public void DealDamage()
        {
			//only melee units play audio when the attack deals damage
			if(attackType == AttackType.Melee)

			if(OnDealDamage != null) OnDealDamage(this);
		}
		public void FireProjectile()
        {
			//ranged units play audio when the projectile is fired

			//if(OnProjectileFired != null) OnProjectileFired(this);
		}

        public virtual void Seek()
        {
            state = States.Seeking;
        }

        protected void TargetIsDead(Placeable p)
        {
            //Debug.Log("My target " + p.name + " is dead", gameObject);
            state = States.Idle;
            target.OnDie -= TargetIsDead;
          
            timeToActNext = lastBlowTime + attackRatio;
        }
        
        public bool IsTargetInRange()
        {
            //return (transform.position-target.transform.position).sqrMagnitude <= attackRange*attackRange;
            return target != null && (transform.position - target.transform.position).sqrMagnitude <= attackRange * attackRange;
        }

        public float SufferDamage(float amount)
        {
            hitPoints -= amount;
            //Debug.Log("Suffering damage, new health: " + hitPoints, gameObject);
            if(state != States.Dead && hitPoints <= 0f || hitPoints <= -2f)
            {
                Die();
            }
            return hitPoints;
        }

		public virtual void Stop()
		{
			state = States.Idle;
        }

        protected virtual void Die()
        {
            state = States.Dead;

			if(OnDie != null) OnDie(this);
        }
    }
}
