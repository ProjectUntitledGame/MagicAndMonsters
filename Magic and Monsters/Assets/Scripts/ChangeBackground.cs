using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    public Image backGround;
    public Sprite changeImage;
    
    public void ChangeSprite()
    {
        backGround.sprite = changeImage;
    }

}
