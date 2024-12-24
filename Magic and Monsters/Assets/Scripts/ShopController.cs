using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [Header("Shop")]
    public int identification;
    private GameObject globalData;

    private void Start()
    {
        globalData = GameObject.FindGameObjectWithTag("GlobalData");
    }
    public GameObject confirm;
    public Image display;
    public bool diceOrSkin;
    public void Buy()
    {
        if (diceOrSkin)
        {
            if (PlayerPrefs.GetInt(identification + " Dice") != 1)
            {
                display.sprite = globalData.GetComponent<ShopSave>().playerDice[identification];
                globalData.GetComponent<ShopSave>().tempIndex = identification;
                globalData.GetComponent<ShopSave>().diceOrSkin = diceOrSkin;
                confirm.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetInt("CurrentDice", identification);
            }
        }
        else
        {
            if (PlayerPrefs.GetInt(identification + " Skin") != 1)
            {
                display.sprite = globalData.GetComponent<ShopSave>().playerSprite[identification];
                globalData.GetComponent<ShopSave>().tempIndex = identification;
                confirm.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetInt("CurrentSkin", identification);
            }
        }
        
        
    }

    public void MicroTransaction()
    {
        globalData.gameObject.GetComponent<ShopSave>().BuyConfirm();
    }
}
