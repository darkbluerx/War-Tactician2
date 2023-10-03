using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CastleDefence
{
    public class PlayerCash : MonoBehaviour
    {
        public static PlayerCash Instance { get; private set; } // Singleton

        [SerializeField] UnitCostSO unitCost;

        [Header("Buttons")]
        public Button spearmanButton;
        public Button tankButton;
        public Button rangerButton;
        public Button cannoneerButton;
        public Button scoutButton;

        [Header("Player Cash")]
        [SerializeField] float playerMoney = 300f;
        [SerializeField] TMP_Text currentMoneyText;

        private bool changeColor = false;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnEnable()
        {
            spearmanButton.onClick.AddListener(() => CostUnit(unitCost.lancerCost, UnitManager.Instance.BuySpearButton));
            tankButton.onClick.AddListener(() => CostUnit(unitCost.shieldmanCost, UnitManager.Instance.BuyTankButton));
            rangerButton.onClick.AddListener(() => CostUnit(unitCost.bowmanCost, UnitManager.Instance.BuyRangerButton));
            cannoneerButton.onClick.AddListener(() => CostUnit(unitCost.cannonCost, UnitManager.Instance.BuyCannoneerButton));
            scoutButton.onClick.AddListener(() => CostUnit(unitCost.scoutCost, UnitManager.Instance.BuyScoutButton));
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
            currentMoneyText.color = changeColor ? Color.green : new Color(253, 189, 0, 255);
            currentMoneyText.text = (changeColor ? "+Gold:" : "Gold: ") + ((int)playerMoney).ToString();
            if (changeColor)
                Invoke("ColorTimer", 0.8f);
        }

        private void ColorTimer()
        {
            changeColor = false;
        }

        private void CostUnit(float cost, System.Action buyAction)
        {
            if (playerMoney >= cost)
            {
                buyAction.Invoke();
                playerMoney -= cost;
                UpdateMoneyText();
            }
        }
    }
}
