using System.Collections.Generic;
using System.Globalization;
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
            spearmanButton.onClick.AddListener(() => CostUnit(UnitCostSO.UnitFamily.Lancer, spearmanButton, UnitManager.Instance.BuySpearButton));
            tankButton.onClick.AddListener(() => CostUnit(UnitCostSO.UnitFamily.Shield, tankButton, UnitManager.Instance.BuyTankButton));
            rangerButton.onClick.AddListener(() => CostUnit(UnitCostSO.UnitFamily.Bow, rangerButton, UnitManager.Instance.BuyRangerButton));
            cannoneerButton.onClick.AddListener(() => CostUnit(UnitCostSO.UnitFamily.Cannon, cannoneerButton, UnitManager.Instance.BuyCannoneerButton));
            scoutButton.onClick.AddListener(() => CostUnit(UnitCostSO.UnitFamily.Scout, scoutButton, UnitManager.Instance.BuyScoutButton));

            RefreshButtonCosts();
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

        private void CostUnit(UnitCostSO.UnitFamily family, Button button, System.Action buyAction)
        {
            float cost = GetCurrentCost(family, button); // the same price the button label shows

            if (playerMoney >= cost)
            {
                buyAction.Invoke();
                playerMoney -= cost;
                UpdateMoneyText();
            }
        }

        // Price of the troop that this gameplay button currently offers (depends on the tier bought in the shop).
        public float GetCurrentCost(UnitCostSO.UnitFamily family, Button button)
        {
            int tier = 0;

            if (BattleTroopReqruitments2.Instance != null && button != null)
                tier = BattleTroopReqruitments2.Instance.GetDisplayedTier(button.GetComponent<GameplayButton>());

            return unitCost.GetCost(family, tier);
        }

        // Writes the real recruit price on every gameplay button. Called at start
        // and again every time the shop changes the troop shown on a button.
        public void RefreshButtonCosts()
        {
            SetButtonCostText(spearmanButton, UnitCostSO.UnitFamily.Lancer);
            SetButtonCostText(tankButton, UnitCostSO.UnitFamily.Shield);
            SetButtonCostText(rangerButton, UnitCostSO.UnitFamily.Bow);
            SetButtonCostText(cannoneerButton, UnitCostSO.UnitFamily.Cannon);
            SetButtonCostText(scoutButton, UnitCostSO.UnitFamily.Scout);
        }

        private void SetButtonCostText(Button button, UnitCostSO.UnitFamily family)
        {
            if (button == null) return;

            GameplayButton gameplayButton = button.GetComponent<GameplayButton>();
            if (gameplayButton == null || gameplayButton.unitCostText == null) return;

            gameplayButton.unitCostText.text = GetCurrentCost(family, button).ToString("0.##", CultureInfo.InvariantCulture);
        }
    }
}
