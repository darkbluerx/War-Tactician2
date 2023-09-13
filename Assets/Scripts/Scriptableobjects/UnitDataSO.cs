//using CastleDefence;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitData", menuName = "Unit/Data")]
public class UnitDataSO : ScriptableObject
{
    //[Header("Unit Type")]
    //public UnitType unitType;
    //public enum UnitType
    //{
    //    spearman,
    //    tank,
    //    ranger
    //}

    //[Header("Unit Stats")]
    [Range(0, 200f)] public float health;
    [Range(0, 200f)] public float damage;
    [Range(0, 200f)] public float attackDelay;
    [Range(0, 200f)] public float attackRange;

    public string unitName;
    public float unitCost;
    public Sprite icon; //button picture

    //public Sprite iconWallpaper; //button picture

    //[Header("Units and Buildings")]
    //public UnitOperations.AttackType attackType = UnitOperations.AttackType.Melee;
    //public UnitBasic.UnitTarget targetType = UnitBasic.UnitTarget.Both;

    //[Header("AudioEvents")]
    //public AudioClip attackClip, dieClip;

    //[Header("NavMeshAgent Data")]

    //[Header("Steering")]
    //public float baseOffSet;

    //public float speed = 4f;
    //public float anguralSpeed = 120;
    //public float acceleration = 8f;
    //public float stoppingDistance = 1f;

    //[Header("Obstacle Avoidance")]
    //public float radius = 0.3f;
    //public float height = 2f;
}
