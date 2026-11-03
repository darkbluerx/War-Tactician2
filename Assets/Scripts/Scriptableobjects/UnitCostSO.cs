using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitCost", menuName = "Unit/Cost")]
public class UnitCostSO : ScriptableObject
{
    // The five recruitable troop lines. Every line has three tiers (0 = 1st, 1 = 2nd, 2 = 3rd).
    public enum UnitFamily { Lancer, Shield, Bow, Cannon, Scout }

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

    // Single source of truth for the recruit price.
    // The price label on the gameplay buttons and the money deduction both call this,
    // so what the button shows is always what the player pays.
    public float GetCost(UnitFamily family, int tier)
    {
        tier = Mathf.Clamp(tier, 0, 2);

        switch (family)
        {
            case UnitFamily.Lancer: return Pick(tier, lancerCost, bodyguardCost, knightCost);
            case UnitFamily.Shield: return Pick(tier, shieldmanCost, mediumShielmanCost, heavyGuardCost);
            case UnitFamily.Bow:    return Pick(tier, bowmanCost, crosbowHunterCost, handGonneCost);
            case UnitFamily.Cannon: return Pick(tier, cannonCost, scorpionCannonCost, lightningCannonCost);
            case UnitFamily.Scout:  return Pick(tier, scoutCost, ninjaCost, samuraiCost);
            default:                return 0f;
        }
    }

    private static float Pick(int tier, float first, float second, float third)
    {
        return tier == 0 ? first : (tier == 1 ? second : third);
    }
}
