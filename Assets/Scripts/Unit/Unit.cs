using System;
using UnityEngine;
using UnityEngine.AI;

namespace CastleDefence
{
    [RequireComponent(typeof(NavMeshAgent),typeof(Animator))]
    //humanoid or anyway a walking placeable
    public class Unit : ThinkingPlaceable
    {
        public event EventHandler OnDanceAnimation;

        [Header("Audios")]
        public AudioEvent attackSoundEvent;
        public AudioEvent deathSoundEvent;
        public AudioEvent battlecrySoundEvent;
        [Space]

        [Header("AudioSources")]
        [SerializeField] AudioSource attackSource;
        [SerializeField] AudioSource deathSource;
        [SerializeField] AudioSource battlecrySource;

        public event EventHandler OnIdleAnimation;
        public event EventHandler OnRunAnimation;
        public event EventHandler OnAttackAnimation;
        public event EventHandler OnDeathAnimation;
      
        //public event EventHandler OnAudioEvent;

        //data coming from the PlaceableData
        private float speed;

        NavMeshAgent navMeshAgent;

        private void Awake()
        {
            pType = Placeable.PlaceableType.Unit;

            attackSource = GameObject.Find("AttackSource").GetComponent<AudioSource>();
            deathSource = GameObject.Find("DeathSource").GetComponent<AudioSource>();
            battlecrySource = GameObject.Find("BattlecrySource").GetComponent<AudioSource>();

            navMeshAgent = GetComponent<NavMeshAgent>(); //will be disabled until Activate is called
        }

        private void Start()
        {
            var randomNumber = UnityEngine.Random.Range(1, 10);
            if (randomNumber == 9) battlecrySoundEvent.Play(battlecrySource);
        }

        //called by GameManager when this Unit is played on the play field
        public void Activate(Faction pFaction, PlaceableData pData)
        {
            faction = pFaction;
            hitPoints = pData.hitPoints;
            targetType = pData.targetType;
            attackRange = pData.attackRange;
            attackRatio = pData.attackRatio;
            speed = pData.speed;
            damage = pData.damagePerAttack;

            navMeshAgent.speed = speed;
            OnIdleAnimation?.Invoke(this, EventArgs.Empty); //will act as multiplier to the speed of the run animation clip

            state = States.Idle;
            navMeshAgent.enabled = true;
        }

        public override void SetTarget(ThinkingPlaceable t)
        {    
            base.SetTarget(t);
            //else target = null;
            //OnRunAnimation?.Invoke(this, EventArgs.Empty);
        }

        //Unit moves towards the target
        public override void Seek()
        {
            if (target == null) return;

            base.Seek();

            navMeshAgent.SetDestination(target.transform.position);
            navMeshAgent.isStopped = false;
            OnRunAnimation?.Invoke(this, EventArgs.Empty);
        }

        //Unit has gotten to its target. This function puts it in "attack mode", but doesn't delive any damage (see DealBlow)
        public override void StartAttack()
        {
            base.StartAttack();
            navMeshAgent.isStopped = true;
        }

        //Starts the attack animation, and is repeated according to the Unit's attackRatio
        public override void DealBlow()
        {
            base.DealBlow();

            //AudioManager.Instance.PlayAttackSound(AudioManager.Unitx.ranger);
            attackSoundEvent.Play(attackSource);

            OnAttackAnimation?.Invoke(this, EventArgs.Empty);
            transform.forward = (target.transform.position - transform.position).normalized; //turn towards the target
        }

        public override void Stop()
        {
            base.Stop();
            navMeshAgent.isStopped = true;
            OnDanceAnimation?.Invoke(this, EventArgs.Empty);
        }

        protected override void Die()
        {
            base.Die();

            deathSoundEvent.Play(deathSource);
            navMeshAgent.enabled = false;
            OnDeathAnimation?.Invoke(this, EventArgs.Empty);

            if (faction == Faction.Player) OpponentGetMoney();
            //if (faction == Faction.Opponent) OpponentGetMoney();
            else PlayerGetMoney();
        }

        public void PlayerGetMoney()
        {
            PlayerCash.Instance.GetMoney1();
        }

        public void OpponentGetMoney()
        {
            OpponentCash.Instance.GetMoney();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
