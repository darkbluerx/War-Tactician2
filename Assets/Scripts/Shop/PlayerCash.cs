using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CastleDefence
{
    public class PlayerCash : MonoBehaviour
    {
        public static PlayerCash Instance { get; private set; } //Singleton

        [SerializeField] UnitCostSO unitCost;

        [Header("Buttons")]
        public Button spearmanButton;
        public Button tankButton;
        public Button rangerButton;

        [Header("Player Cash")]
        float playerMoney = 300f;
        [SerializeField] TMP_Text currentMoneyText;

        bool changeColor = false;

        private void Awake()
        {
            if (Instance != null)
            {
                //Debug.LogError("There's more than one PlayerCash! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;   
        }

        private void OnEnable()
        {
            //Buy units
            spearmanButton.onClick.AddListener(GetSpearman);
            tankButton.onClick.AddListener(GetTank);
            rangerButton.onClick.AddListener(GetRanger);
        }

        private void FixedUpdate()
        {
            Invoke("MoneyGrowth", 1f);           
        }

        private void MoneyGrowth()
        {
            playerMoney += unitCost.playerMoneyFactor * Time.deltaTime;

            UpdateMoneyText();
        }

        public void GetMoney1() //Reward for killing troops
        {
            changeColor = true;
            UpdateMoneyText();

            playerMoney += unitCost.killMoney;
        }

        private void UpdateMoneyText()
        {
            if (changeColor == false) 
            {
                currentMoneyText.color = new Color(253, 189, 0, 255);
                currentMoneyText.text = "Gold: " + ((int)playerMoney).ToString();
            }
            if(changeColor == true)
            {
                //getMoney.Play(source);
                currentMoneyText.color = Color.green;
                currentMoneyText.text = "+Gold:" + ((int)playerMoney).ToString();
                Invoke("ColorTimer", 0.8f);             
            } 
        }

        private void ColorTimer()
        {
            changeColor = false;
        }

        private void GetSpearman()
        {
            CostSpearman();
        }

        private void GetTank()
        {
            CostTank();
        }

        private void GetRanger()
        {
            CostRanger();
        }

        public void CostSpearman()
        {
            if (playerMoney >= unitCost.lancerCost)
            {
                UnitManager.Instance.BuySpearButton(); //Instantiate unit
                playerMoney -= unitCost.lancerCost;
                UpdateMoneyText();           
            }
        }

        private void CostTank()
        {
            if (playerMoney >= unitCost.shieldmanCost)
            {
                UnitManager.Instance.BuyTankButton(); //Instantiate unit
                playerMoney -= unitCost.shieldmanCost;
                UpdateMoneyText();
                //OnBuyTank?.Invoke(this, EventArgs.Empty);
            }
        }

        private void CostRanger()
        {
            if (playerMoney >= unitCost.bowmanCost)
            {
                UnitManager.Instance.BuyRangerButton(); //Instantiate unit
                playerMoney -= unitCost.bowmanCost;
                UpdateMoneyText();
               //OnBuyRanger?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

