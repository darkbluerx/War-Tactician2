using CastleDefence;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpponentCash : MonoBehaviour
{
    public static OpponentCash Instance { get; private set; } //Singleton

    [HideInInspector] public bool IsEnemy = false;

    [SerializeField] UnitCostSO unitCost;
    
    [Header("Opponent Cash")]
    [SerializeField] float opponentMoney = 300f;
    [SerializeField] TMP_Text currentopponentMoneyText;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one OpponentCash! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)) BuyUnit();
    }

    private void FixedUpdate()
    {
        MoneyGrowth();
        BuyUnit();
    }

    private void MoneyGrowth()
    {
        opponentMoney += unitCost.opponentMoneyFactor * Time.deltaTime;

        UpdateopponentMoneyText();
    }

    private void UpdateopponentMoneyText()
    {
        currentopponentMoneyText.text = opponentMoney.ToString();
    }

    public void GetMoney() //Reward for killing troops
    {
        opponentMoney += unitCost.killMoney;
    }

    public bool Enemy()
    {
        return IsEnemy;
    }

    public void BuyUnit()
    {
        //Debug.Log("BuyUnit/OpponentCash");
       
        if (opponentMoney >= 40)
        {
            IsEnemy = true;
            UnitManager.Instance.BuySpearButton();

            opponentMoney -= unitCost.spearmanCost;
            UpdateopponentMoneyText();
        }

        if (opponentMoney >= 55)
        {
            IsEnemy = true;
            UnitManager.Instance.BuyTankButton();
            opponentMoney -= unitCost.tankCost;
            UpdateopponentMoneyText();
        }

        if (opponentMoney >= 80)
        {
            IsEnemy = true;
            UnitManager.Instance.BuyRangerButton();
            opponentMoney -= unitCost.rangerCost;
            UpdateopponentMoneyText();
        }

        IsEnemy = false;
    }
}
