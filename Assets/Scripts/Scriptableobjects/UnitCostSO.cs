using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitCost", menuName = "Unit/Cost")]
public class UnitCostSO : ScriptableObject
{
    [Header("Unit Cost")]
    public float playerMoneyFactor = 5;
    public float opponentMoneyFactor = 5;
    [Space]

    public float lancerCost = 50;
    public float bodyguardCost = 35;
    public float knightCost = 40;
    [Space]

    public float shieldmanCost = 50;
    public float mediumShielmanCost = 35;
    public float heavyGuardCost = 40;
    [Space]

    public float bowmanCost = 50;
    public float crosbowHunterCost = 35;
    public float handGonneCost = 40;
    [Space]

    public float cannonCost = 50;
    public float scorpionCannonCost = 55;
    public float lightningCannonCost = 60;
    [Space]

    public float scoutCost = 50;
    public float ninjaCost = 25;
    public float samuraiCost = 30;

    [Header("Reward for killing troops")]
    public float killMoney = 15f;
}
