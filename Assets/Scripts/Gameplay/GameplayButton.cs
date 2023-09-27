
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayButton : MonoBehaviour
{
    public TMP_Text unitNameText;
    public Image backgroundImage;
    public Image unitImage;

    public TMP_Text unitCostText;

    [SerializeField] Button button;
    //public Image selectedImage;

    //public UnityEvent buttonEvent;

    private void Awake()
    {
        button = GetComponent<Button>(); //get reference to the button component
        //buttonEvent.AddListener(ClickButton);
    }

    public void OnEnable()
    {
        button.onClick.RemoveAllListeners();
        //button.onClick.AddListener(ShowSelectedButton);
        //button.onClick.AddListener(ClickButton);

        //button.onClick.AddListener(ClickButton);
        //Kauppa.Instance.unitButtonEvent.AddListener(ShowSelectedButton);
        //toimii

    }

    //public void ClickButton()
    //{
    //    buttonEvent.Invoke();
    //    selectedImage.enabled = true;
    //}
}
