using CastleDefence;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CastleDefence
{
    //[RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public Unitx unitX;
        public enum Unitx
        {
            spearman,
            tank,
            ranger
        }


        public static AudioManager Instance { get; private set; }

        [Header("AudioSources")]
        [SerializeField] AudioSource attackSource;
        [SerializeField] AudioSource deathSource;
        [SerializeField] AudioSource battlecrySource;

        [Header("AudioEvents")]
        [SerializeField] AudioEvent spearmanAttackSoundEvent, tankAttackSoundEvent, rangerAttackSoundEvent;
        [SerializeField] AudioEvent deathSoundEvent;
        [SerializeField] AudioEvent battlecrySoundEvent;

        private void Awake()
        {
            attackSource = GameObject.Find("AttackSource").GetComponent<AudioSource>();
            deathSource = GameObject.Find("DeathSource").GetComponent<AudioSource>();
            battlecrySource = GameObject.Find("BattlecrySource").GetComponent<AudioSource>();

            if (Instance != null) 
            { 
                Debug.LogError("There's more than one AudiManager! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        //public void PlayAttackSound(Unitx unit)
        //{
        //    if(Unitx.spearman == unit) 
        //    {
        //        spearmanAttackSoundEvent.Play(attackSource);
        //    }
        //    if (Unitx.tank == unit)
        //    {
        //        tankAttackSoundEvent.Play(attackSource);
        //    }
        //    else
        //    {
        //        rangerAttackSoundEvent.Play(attackSource);
        //    }
        //}

        public void PlayAttackSound1()
        {
            spearmanAttackSoundEvent.Play(attackSource);
        }

        public void PlayAttackSound2()
        {
            tankAttackSoundEvent.Play(attackSource);
        }

        public void PlayAttackSound3()
        {
            rangerAttackSoundEvent.Play(attackSource);
        }

        public void PlayDeathSound()
        {
            deathSoundEvent.Play(deathSource);
        }

        public void PlayBattlecrySound1()
        {
            battlecrySoundEvent.Play(battlecrySource);
        }

        public void PlayBattlecrySound2()
        {
            battlecrySoundEvent.Play(battlecrySource);
        }
    }
}
