using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSave : MonoBehaviour
{
    public Sprite[] playerSprite;
    public Sprite[] playerKOSprite;
    public Sprite[] playerDice;
    public int playerSpriteIndex = 2;
    public Image playerImage;
    public Image playerDiceImage;
    public Image playerKOImage;
    public int tempIdentification;

    private void Start()
    {
        if(playerImage != null)
        {
            playerDiceImage.sprite = playerDice[PlayerPrefs.GetInt("CurrentDice")];
            playerSpriteIndex = PlayerPrefs.GetInt("CurrentSkin");
            playerImage.sprite = playerSprite[playerSpriteIndex];
            playerKOImage.sprite = playerKOSprite[playerSpriteIndex];
        }
        
    }
    public int tempIndex;
    public bool diceOrSkin;
    public void BuyConfirm()
    {
        if (diceOrSkin)
        {
            if (PlayerPrefs.GetInt(tempIndex.ToString() + " Dice") != 1)
            {
                PlayerPrefs.SetInt(tempIndex.ToString() + " Dice", 1);
                PlayerPrefs.SetInt("CurrentDice", tempIndex);
            }
            else
            {
                PlayerPrefs.SetInt("CurrentDice", tempIndex);
            }
        }
        else
        {
            if (PlayerPrefs.GetInt(tempIndex.ToString() + " Skin") != 1)
            {
                PlayerPrefs.SetInt(tempIndex.ToString() + " Skin", 1);
                PlayerPrefs.SetInt("CurrentSkin", tempIndex);
            }
            else
            {
                PlayerPrefs.SetInt("CurrentSkin", tempIndex);
            }
        }
        
        
    }

    
}
