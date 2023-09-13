using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTroopRecruitmentPrices", menuName = "Unit/Recruiment Prices")]
public class TroopRecruitmentPricesSO : ScriptableObject
{
    [Header("Unit Cost")]
    public float playerMoneyFactor = 5;
    public float opponentMoneyFactor = 5;
    [Space]

    [Header("Footmen Pictures")]
    public Sprite lancerPic;
    public Sprite bodyguardPic;
    public Sprite knightPic;

    [Header("Defenders Pictures")]
    public Sprite shielmanPic;
    public Sprite mediumShielmandPic;
    public Sprite heavyguardPic;

    [Header("Special Units Pictures")]
    public Sprite berserkerPic;
    public Sprite ninjaPic;
    public Sprite samuraiPic;

    [Header("Archers Pictures")]
    public Sprite bowmanPic;
    public Sprite crossbowHunterPic;
    public Sprite handGonnePic;

    [Header("War Machines Pictures")]
    public Sprite cannonPic;
    public Sprite scrorpionCannonPic;
    public Sprite siegeTowerPic;

    [Header("Footmen Cost")]
    public float lancerCost = 70;
    public float bodyguardCost = 100;
    public float knightCost = 150;
    [Space]

    [Header("Defenders Cost")]
    public float shielmanCost = 70;
    public float mediumShielmanCost = 100;
    public float heavyGuardCost = 150;
    [Space]

    [Header("Special Unit Cost")]
    public float berserkerCost = 70;
    public float ninjaCost = 100;
    public float samuraiCost = 150;
    [Space]

    [Header("Archers Cost")]
    public float bowmanCost = 70;
    public float crossbowHunterCost = 100;
    public float handGonneCost = 150;
    [Space]

    [Header("War Machines Cost")]
    public float cannonCost = 70;
    public float scrorpionCannonCost = 100;
    public float siegeTowerCost = 150;


    public TroopRecruitmentPricesSO Create()
    {
        TroopRecruitmentPricesSO Instance = ScriptableObject.CreateInstance<TroopRecruitmentPricesSO>();
        return CopyBaseValues(Instance);
    }

    public T CopyBaseValues<T>(T Instance) where T : TroopRecruitmentPricesSO
    {
        Instance.playerMoneyFactor = playerMoneyFactor;
        Instance.opponentMoneyFactor = opponentMoneyFactor;

        Instance.lancerCost = lancerCost;
        Instance.bodyguardCost = bodyguardCost;
        Instance.knightCost = knightCost;

        Instance.shielmanCost = shielmanCost;
        Instance.mediumShielmanCost = mediumShielmanCost;
        Instance.heavyGuardCost = heavyGuardCost;
        
        Instance.berserkerCost = berserkerCost;
        Instance.ninjaCost = ninjaCost;
        Instance.samuraiCost = samuraiCost;

        Instance.bowmanCost = bowmanCost;
        Instance.crossbowHunterCost = crossbowHunterCost;
        Instance.handGonneCost = handGonneCost;

        Instance.cannonCost = cannonCost;
        Instance.scrorpionCannonCost = scrorpionCannonCost;
        Instance.siegeTowerCost = siegeTowerCost;

        return Instance;
    }

    public void GetPictures()
    {

    }

    //lähde https://www.youtube.com/watch?v=dIAAi54Ty58



    //public TroopRecruitmentPricesSO(float playerMoneyFactor, float opponentMoneyFactor, Sprite lancerPic, Sprite bodyguardPic, Sprite knightPic, Sprite shielmanPic, Sprite mediumShielmandPic, Sprite heavyguardPic, Sprite berserkerPic, Sprite ninjaPic, Sprite samuraiPic, Sprite bowmanPic, Sprite crossbowHunterPic, Sprite handGonnePic, Sprite cannonPic, Sprite scrorpionCannonPic, Sprite siegeTowerPic, float lancerCost, float bodyguardCost, float knightCost, float shielmanCost, float mediumShielmanCost, float heavyGuardCost, float berserkerCost, float ninjaCost, float samuraiCost, float bowmanCost, float crossbowHunterCost, float handGonneCost, float cannonCost, float scrorpionCannonCost, float siegeTowerCost)
    //{

    //}


}
