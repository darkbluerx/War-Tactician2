using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleDefence
{
    [CreateAssetMenu(fileName = "NewPlaceable", menuName = "CastleDefence/Placeable Data")]
    public class PlaceableData : ScriptableObject
    {
        [Header("Common")]
        public Placeable.PlaceableType pType;
        public GameObject associatedPrefab;
        public GameObject alternatePrefab;
        
        [Header("Units and Buildings")]
        public ThinkingPlaceable.AttackType attackType = ThinkingPlaceable.AttackType.Melee;
        public Placeable.PlaceableTarget targetType = Placeable.PlaceableTarget.Both;
        [Range(0, 10f)] public float attackRatio; //time between attacks
        [Range(0, 100f)] public float damagePerAttack; //damage each attack deals
        [Range(0, 50f)] public float attackRange;
        [Range(0, 500f)] public float hitPoints; //when units or buildings suffer damage, they lose hitpoints
		
        [Header("Units")]
        [Range(0, 100f)] public float speed; //movement speed
        
        //[Header("Audios")]
        //public AudioEvent attackAudioEvent;
        //public AudioEvent deadthAudioEvent;
        //public AudioEvent warcryAudioEvent;
    }
}