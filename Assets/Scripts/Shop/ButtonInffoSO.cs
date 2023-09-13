using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewButtonInffoSO", menuName = "Button/Inffo")]
public class ButtonInffoSO : ScriptableObject
{
    [Header("Unit Image")]
    public Sprite unitPicture;

    [Header("Unit Cost")]
    public int unitCost;

    [Header("Unit Name")]
    public string unitName;
}

