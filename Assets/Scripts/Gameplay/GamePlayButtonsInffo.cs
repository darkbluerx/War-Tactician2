using UnityEngine;
using UnityEngine.UI;

public class GamePlayButtonsInffo : MonoBehaviour
{
    [Header("ButtonInffoSO ScritableObjects")]
    public ButtonInffoSO[] gameplayButtonInffos;
    [Space]

    [Header("Unit Buttons")]
    public GameplayButton[] unitButtons;
    [Space]

    [Header("GamePlay Buttons")]
    public Button[] gameplayButtons; //size 5,
    public GameObject[] gamePlayButtonsGameObjects; //size 5,

    private void OnEnable()
    {
        LoadUnitInffos();
    }

    public void LoadUnitInffos() //Load the unit info to the buttons: images, names and costs
    {
        for (int i = 0; i < gameplayButtonInffos.Length; i++)
        {
            unitButtons[i].unitNameText.text = gameplayButtonInffos[i].unitName;
            unitButtons[i].unitCostText.text = gameplayButtonInffos[i].unitCost.ToString();
            unitButtons[i].backgroundImage.sprite = gameplayButtonInffos[i].backgroundPicture;
            unitButtons[i].unitImage.sprite = gameplayButtonInffos[i].unitPicture;
        }
    }
}

