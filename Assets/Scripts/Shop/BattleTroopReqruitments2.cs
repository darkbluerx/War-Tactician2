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

    private void LoadButtonInffos(int buttonIndex) //Load troop information to the buttons on the Gameplay canvas: images, names and costs
    {
        for (int i = 0; i < buttonInffos.Length; i++)
        {
            if (buttonIndex == 0 || buttonIndex == 1 || buttonIndex == 2)
            {
                //Debug.Log("buttonIndex: " + buttonIndex);
                gameplayUnitButtons[0].unitNameText.text = buttonInffos[buttonIndex].unitName;
                gameplayUnitButtons[0].unitCostText.text = buttonInffos[buttonIndex].unitCost.ToString();
                gameplayUnitButtons[0].backgroundImage.sprite = buttonInffos[buttonIndex].backgroundPicture;
                gameplayUnitButtons[0].unitImage.sprite = buttonInffos[buttonIndex].unitPicture;
            }

            if (buttonIndex == 3 || buttonIndex == 4 || buttonIndex == 5)
            {
                gameplayUnitButtons[1].unitNameText.text = buttonInffos[buttonIndex].unitName;
                gameplayUnitButtons[1].unitCostText.text = buttonInffos[buttonIndex].unitCost.ToString();
                gameplayUnitButtons[1].backgroundImage.sprite = buttonInffos[buttonIndex].backgroundPicture;
                gameplayUnitButtons[1].unitImage.sprite = buttonInffos[buttonIndex].unitPicture;
            }

            if (buttonIndex == 6 || buttonIndex == 7 || buttonIndex == 8)
            {
                gameplayUnitButtons[2].unitNameText.text = buttonInffos[buttonIndex].unitName;
                gameplayUnitButtons[2].unitCostText.text = buttonInffos[buttonIndex].unitCost.ToString();
                gameplayUnitButtons[2].backgroundImage.sprite = buttonInffos[buttonIndex].backgroundPicture;
                gameplayUnitButtons[2].unitImage.sprite = buttonInffos[buttonIndex].unitPicture;
            }

            if (buttonIndex == 9 || buttonIndex == 10 || buttonIndex == 11)
            {
                gameplayUnitButtons[3].unitNameText.text = buttonInffos[buttonIndex].unitName;
                gameplayUnitButtons[3].unitCostText.text = buttonInffos[buttonIndex].unitCost.ToString();
                gameplayUnitButtons[3].backgroundImage.sprite = buttonInffos[buttonIndex].backgroundPicture;
                gameplayUnitButtons[3].unitImage.sprite = buttonInffos[buttonIndex].unitPicture;
            }

            if (buttonIndex == 12 || buttonIndex == 13 || buttonIndex == 14)
            {
                gameplayUnitButtons[4].unitNameText.text = buttonInffos[buttonIndex].unitName;
                gameplayUnitButtons[4].unitCostText.text = buttonInffos[buttonIndex].unitCost.ToString();
                gameplayUnitButtons[4].backgroundImage.sprite = buttonInffos[buttonIndex].backgroundPicture;
                gameplayUnitButtons[4].unitImage.sprite = buttonInffos[buttonIndex].unitPicture;
            }
        }
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
