using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Test : MonoBehaviour
{


    private void Awake()
    {
        Create();
    }

    //float playerMoneyFactor2;


    public TroopRecruitmentPricesSO Create()
    {
        TroopRecruitmentPricesSO Instance = ScriptableObject.CreateInstance<TroopRecruitmentPricesSO>();
        return CopyBaseValues(Instance);
    }


    public T CopyBaseValues<T>(T Instance) where T : TroopRecruitmentPricesSO
    {
        Instance.playerMoneyFactor = Instance.playerMoneyFactor;
        ++Instance.playerMoneyFactor;
        Debug.Log("Test " + Instance.playerMoneyFactor);
        return Instance;
    }
}
