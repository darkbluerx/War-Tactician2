using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

public class RecruitmentOfTroops : MonoBehaviour
{
    public static  RecruitmentOfTroops Instance { get; private set; }

    [SerializeField] ButtonInffoSO buttonInffo;

    [Header("")]
    [SerializeField] TMP_Text player1Text;
    [SerializeField] TMP_Text player2Text;

    public bool haveMoney = false;

    public float player1Money; //{ get; private set; } //you can edit only from this class
    public float player2Money; // { get; private set; }

    public List<Button> buttons = new List<Button>();

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There's more tahn one RecruitmentOfTroops!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateMoneyText();
    }

    private void OnEnable()
    {
        
    }

    public void UnitCost()
    {
        if (player1Money >= buttonInffo.unitCost)       
        {
            haveMoney = true;
            player1Money -= buttonInffo.unitCost;
            UpdateMoneyText();
            Debug.Log(player1Money.ToString());

            //button.image.color = Color.green;

            //button.enabled = false;
            //bodyguardButton.gameObject.SetActive(true);
        }
        haveMoney = false;
    }

    private void UpdateMoneyText()
    {
        player1Text.text = "Gold: " + ((int)player1Money).ToString() ;
        player2Text.text = "Gold: " + ((int)player2Money).ToString();

        //UnitButton.GetUnitCost();
    }
}
