using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatModfier : MonoBehaviour
{
    //public int stat_id;

    public Text stat_quant;
    [HideInInspector] int quant = 0;
    public int Quant { get { return quant; } set { quant = value; } }

    public GameObject plusButton;

    public void Plus()
    {
        quant++;
        UpdateQuant();

        GameManager.instance.MainHud.UpdateAvailablePoints(1);
    }

    public void Minus()
    {
        quant--;
        UpdateQuant();
    }

    public void UpdateQuant()
    {
        stat_quant.text = quant.ToString();
    }

    public void HidePlusButton()
    {
        plusButton.SetActive(false);
    }
}
