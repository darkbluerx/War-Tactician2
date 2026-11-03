using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class BattleTroopReqruitments2 : MonoBehaviour
{
    public static BattleTroopReqruitments2 Instance { get; private set; } //Singleton

    [Header("Player Money/Gold")]
    public int player1Money;
    [Space]
 
    public TMP_Text player1MoneyText;
    [Space]

    [Header("ButtonInffoSO ScritableObjects")]
    public ButtonInffoSO[] buttonInffos;
    [Space]

    [Header("Turn on/off Button GameObjects")]
    public GameObject[] buttonGameobjects;
    [Space]

    [Header("Unit Buttons")]
    public UnitButton[] unitButtons;
    public Button[] myPurchaseButtons;
    public Image[] myPurchaseButtonsImages;
    [Space]

    [Header("Buttons")]
    public GameplayButton[] gameplayUnitButtons;
    [Space]

    [Header("GamePlay Buttons")]
    public Button[] gamePlayButtons; //size 5,
    public GameObject[] gamePlayButtonsGameObjects; //size 5,

    public UnityEvent CallUnlockUnit;

    bool is2TierPurchased = false;

    // Tier (0 = 1st, 1 = 2nd, 2 = 3rd) that each gameplay button shows right now. PlayerCash reads this to price the troop.
    readonly int[] displayedTier = new int[5];

    private void Awake()
    {
        if (Instance != null) //Singleton pattern
        {
            //Debug.LogError("There's more than one Kauppa! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Hide2TierUnits(); //Hide the 2nd tier units. You have to buy the 1st tier units first
        Hide3TierUnits(); //Hide the 3rd tier units. You have to buy the 2nd tier units first

        //gameplayCanvas.SetActive(false);

        for (int i = 0; i < gamePlayButtons.Length; i++)
        {
            gamePlayButtons[i].interactable = false;
        }
    }

    private void OnEnable()
    {
        //Show the buttons gameobjects
        for (int i = 0; i < buttonInffos.Length; i++)
        {
            buttonGameobjects[i].SetActive(true);
        }

        UpdateMoneyText();
        LoadUnitInffos();
        //CheckPurchaseable();

        //Purchase button presses
        for (int i = 0; i < myPurchaseButtons.Length; i++)
        {
            int buttonIndex = i; // Capture the index in a local variable
            myPurchaseButtons[i].onClick.RemoveAllListeners();
            myPurchaseButtons[i].onClick.AddListener(() => PurchaseUnit(buttonIndex));
        }
    }

    private void Hide2TierUnits()
    {
        for (int i = 0; i < myPurchaseButtons.Length; i++)
        {
            if (i == 1 || i == 4 || i == 7 || i == 10 || i == 13)
            {
                myPurchaseButtons[i].interactable = false;
            }
        }
    }

    private void Hide3TierUnits()
    {
        for (int i = 0; i < myPurchaseButtons.Length; i++)
        {
            if (i == 2 || i == 5 || i == 8 || i == 11 || i == 14)
            {
                myPurchaseButtons[i].interactable = false;
            }
        }
    }
    private void UpdateMoneyText()
    {
        player1MoneyText.text = "Gold " + player1Money.ToString();       
    }

    public void AddMoney()
    {
        player1Money++;
        UpdateMoneyText();
        //CheckPurchaseable(buttonIndex);
    }

    public void CheckPurchaseable(int buttonIndex)
    {
        for (int i = 0; i < buttonInffos.Length; i++)
        {

            if (player1Money >= buttonInffos[i].unitCost)
            {
                myPurchaseButtons[buttonIndex].interactable = false;

                HasBeenPurchased(buttonIndex);

                Unlock2TierUnits(buttonIndex);
                if (is2TierPurchased)
                {
                    Unlock3TierUnits(buttonIndex);
                }
            }
            else
            {
                myPurchaseButtons[i].interactable = false;
                HasBeenPurchased(buttonIndex); //Highlight image of the purchase if you have only 50 money
            }
        }
    }

    bool HasBeenPurchased(int buttonIndex) //Highlight image of the purchase
    {
        myPurchaseButtonsImages[buttonIndex].enabled = true;
        return true;
    }

    private void Unlock2TierUnits(int buttonIndex)
    {
        if (buttonIndex == 0 || buttonIndex == 3 || buttonIndex == 6 || buttonIndex == 9 || buttonIndex == 12)
        {
            myPurchaseButtons[buttonIndex + 1].interactable = true;
            is2TierPurchased = true;
        }
    }

    private void Unlock3TierUnits(int buttonIndex)
    {
        if (buttonIndex == 1 || buttonIndex == 4 || buttonIndex == 7 || buttonIndex == 10 || buttonIndex == 13)
        {
            myPurchaseButtons[buttonIndex + 1].interactable = true;
        }
    }

    public void PurchaseUnit(int buttonIndex)
    {
        
        if (player1Money >= buttonInffos[buttonIndex].unitCost)
        {
            player1Money -= buttonInffos[buttonIndex].unitCost;
            UpdateMoneyText();
            CheckPurchaseable(buttonIndex);
            UnlockUnit(buttonIndex);
        }
    }

    public void UnlockUnit(int buttonIndex)
    {     
        int gamePlayButtonIndex = buttonIndex / 3; // Calculate the gamePlayButtons index

        if (gamePlayButtonIndex >= 0 && gamePlayButtonIndex < gamePlayButtons.Length)
        {
            gamePlayButtons[gamePlayButtonIndex].interactable = true;
            //gamePlayButtonsGameObjects[gamePlayButtonIndex].SetActive(true);

            LoadButtonInffos(buttonIndex);
        }

    }

    private void InstantiateButton()
    {
        Instantiate(gamePlayButtons[0], gamePlayButtons[0].transform.position, Quaternion.identity);
    }

    private void LoadButtonInffos(int buttonIndex) //Load troop information to the buttons on the Gameplay canvas: images and names. The price comes from PlayerCash (see below)
    {
        int slot = buttonIndex / 3; // 3 tiers per troop type -> index of the gameplay button (0-4)

        if (slot < 0 || slot >= gameplayUnitButtons.Length || slot >= displayedTier.Length) return;

        gameplayUnitButtons[slot].unitNameText.text = buttonInffos[buttonIndex].unitName;
        gameplayUnitButtons[slot].backgroundImage.sprite = buttonInffos[buttonIndex].backgroundPicture;
        gameplayUnitButtons[slot].unitImage.sprite = buttonInffos[buttonIndex].unitPicture;

        // buttonInffos[..].unitCost is the SHOP (unlock) price, not the price of recruiting the troop in battle,
        // so it is not written to the gameplay button. Remember the tier instead and let PlayerCash show the real price.
        displayedTier[slot] = buttonIndex % 3;

        if (CastleDefence.PlayerCash.Instance != null)
            CastleDefence.PlayerCash.Instance.RefreshButtonCosts();
    }

    // Tier (0 = 1st, 1 = 2nd, 2 = 3rd) that the given gameplay button shows. 0 if the button is not one of ours.
    public int GetDisplayedTier(GameplayButton button)
    {
        if (button == null) return 0;

        for (int i = 0; i < gameplayUnitButtons.Length && i < displayedTier.Length; i++)
        {
            if (gameplayUnitButtons[i] == button) return displayedTier[i];
        }

        return 0;
    }

    public void LoadUnitInffos() //Load the unit info to the buttons (Shop canvas): images, names and costs
    {
        for (int i = 0; i < buttonInffos.Length; i++)
        {
            //Blue Team buttons (left side)
            unitButtons[i].unitNameText.text = buttonInffos[i].unitName;
            unitButtons[i].unitCostText.text = buttonInffos[i].unitCost.ToString();
            unitButtons[i].backgroundImage.sprite = buttonInffos[i].backgroundPicture;
            unitButtons[i].unitImage.sprite = buttonInffos[i].unitPicture;
        }
    }
}
