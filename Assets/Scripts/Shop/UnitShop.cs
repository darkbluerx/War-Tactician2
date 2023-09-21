using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitShop : MonoBehaviour
{
    public static UnitShop Instance  { get; private set; } //Singleton

    [SerializeField] TroopRecruitmentPricesSO price;


    //hae find käkyllä näppäimet ja kuvat
    

    [Header("Player 1 Buttons")]
    [SerializeField] Button lancerButton;
    [SerializeField] Button bodyguardButton;
    [SerializeField] Button knightButton;
    [Space]

    [Header("Player 1 Buttons")]
    [SerializeField] Image lancerImage;
    [SerializeField] Image bodyguardImage;
    [SerializeField] Image knightImage;

    [Space]

    [Header("Player 1 Cash")]
    [SerializeField] float playerMoney = 1000f;
    [SerializeField] TMP_Text currentMoneyText;

    [Header("Player 2 Cash")]
    //[SerializeField] float player2Money = 1000f;
    [SerializeField] TMP_Text currentMoney2Text;


    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There's more than one UnitShop! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Button[] buttons = GameObject.FindObjectsOfType<Button>();
        List<GameObject> list = new List<GameObject>();
    }


    private void OnEnable()
    {
        //Buy units

        if (true)
        {
            lancerButton.onClick.AddListener(BuyUnit);
        }
        
        bodyguardButton.onClick.AddListener(BuyBodyguard);
        knightButton.onClick.AddListener(BuyKnight);
    }

    public List<ButtonInffoSO> buttonInffoSOs = new List<ButtonInffoSO>();

    private void BuyUnit()
    {
        //if (playerMoney >= ButtonInffoSO.unitCost)
        //{
        //    playerMoney -= price.lancerCost;
        //    UpdateMoneyText();

        //    lancerImage.color = Color.green;

        //    lancerButton.enabled = false;
        //    bodyguardButton.gameObject.SetActive(true);
        //}
    }

    private void BuyLancer()
    {
        CostLancer();
    }

    private void BuyBodyguard()
    {
        CostBodyguard();
    }

    private void BuyKnight()
    {
        CostKnight();
    }

    private void CostLancer()
    {
        if (playerMoney >= price.lancerCost)
        {
            playerMoney -= price.lancerCost;
            UpdateMoneyText();

            lancerImage.color = Color.green;

            lancerButton.enabled = false;
            bodyguardButton.gameObject.SetActive(true);
        } 
    }

    private void CostBodyguard()
    {
        if (playerMoney >= price.bodyguardCost)
        {
            playerMoney -= price.bodyguardCost;
            UpdateMoneyText();

            lancerImage.color = Color.gray;
            //bodyguardImage.color = Color.green;

            bodyguardButton.enabled = false;
            knightButton.gameObject.SetActive(true);
        }
    }

    private void CostKnight()
    {
        if (playerMoney >= price.lancerCost)
        {
            playerMoney -= price.lancerCost;
            UpdateMoneyText();
            knightButton.enabled = false;
            bodyguardButton.gameObject.SetActive(true);
        }
    }



    private void UpdateMoneyText()
    {
        //getMoney.Play(source);
        currentMoneyText.color = Color.green;
        currentMoneyText.text = "+Gold:" + ((int)playerMoney).ToString();
        Invoke("ColorTimer", 0.8f);
    }


   //float playerMoneyFactor2;


   // public T CopyBaseValues<T>(T Instance) where T : TroopRecruitmentPricesSO
   // {
   //     Instance.playerMoneyFactor = playerMoneyFactor2;
   //     Debug.Log(playerMoneyFactor2);
   //     return Instance;
   // }

    //public TroopRecruitmentPricesSO Create()
    //{
    //    TroopRecruitmentPricesSO Instance = ScriptableObject.CreateInstance<TroopRecruitmentPricesSO>();
    //    return CopyBaseValues(Instance);
    //}

}
