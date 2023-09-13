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
    public float spearmanCost = 50;
    public float tankCost = 100;
    public float rangerCost = 200;
    [Space]

    [Header("Reward for killing troops")]
    public float killMoney = 35f;
}
