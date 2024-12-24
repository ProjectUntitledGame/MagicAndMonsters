using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellInfo : MonoBehaviour
{
    public string spellName;
    public int power;
    public Text text;
    private GlobalData gd;
    private bool attacking = false;
    public int buttonIndex;
    public bool start;
    public void SetButton()
    {
        gd.gameObject.GetComponent<SpellBook>().tempSpell = this;
    }
    private void Start()
    {
        gd = GameObject.FindGameObjectWithTag("GlobalData").GetComponent<GlobalData>();
        if (!start)
        {
            text.text = spellName;
            spellDisplay = gd.spellDisplay;
            hitRequired = gd.hitRequired;
            damageDie = gd.damageDie;
        }
        
    }

    public void SetTempSpell()
    {
        gd.GetComponent<SpellBook>().tempSpell = this;
    }

    private Text spellDisplay;
    private Text hitRequired;
    private Text damageDie;
    public void SelectSpell()
    {
        if (gd.alive && gd.turn)
        {
            spellDisplay.text = spellName;
            hitRequired.text = "Roll to Hit: " + gd.gameObject.GetComponent<SpellBook>().spells[spellName].hitRequired.ToString();
            RollForHit();
        }
        
    }

    public void RollForHit()
    {
        gd.displayResult = gd.roll;
        StartCoroutine("RollTheDice",false);

    }
    public void StartInit()
    {
        gd.displayResult = gd.initResult;
        StartCoroutine("RollTheDice",true);
    }
    public void GetResult(int result, bool initiative)
    {
        if (!initiative)
        {
            if (gd.turn)
            {
                gd.turn = false;
                if (result < 10)
                {
                    gd.hitOrMiss.text = "MISS!";
                    miss = true;
                }
                else
                {
                    gd.StartCoroutine("RollHit", spellName);
                    miss = false;
                    gd.hitOrMiss.text = "HIT!";
                    gd.tempEnemyHealth -= 50;
                    attacking = true;
                }
                if (gd.tempEnemyHealth <= 0)
                {
                    gd.winScreen.SetActive(true);
                    gd.GetReward();
                    if (PlayerPrefs.GetInt(PlayerPrefs.GetString("tempLevelName")) < gd.level + 1)
                    {
                        PlayerPrefs.SetInt(PlayerPrefs.GetString("tempLevelName"), gd.level + 1);
                    }
                    gd.enemyImage.sprite = gd.enemyKOSprites[gd.spriteIndex];
                }
            }
        }
        else
        {
            gd.playerInit = result;
            gd.displayResult = gd.initResult;
            init = true;
            
            
        }
        
    }

    private IEnumerator RollTheDice(bool initiative)
    {
        int temp = 0;
        for (int i = 0; i <= 10; i++)
        {
            temp = Random.Range(0, 19);
            gd.displayResult.text = temp.ToString();
             

            yield return new WaitForSeconds(0.05f);
        }

        int finalSide = temp + 1;
        gd.displayResult.text = finalSide.ToString();
        GetResult(finalSide, initiative);
    }


    private bool miss;
    private float delay = 1;
    private bool init;
    private void Update()
    {
        if (!init)
        {
            if (gd.slider != null)
            {
                if (miss)
                {
                    if (delay > 0)
                    {
                        if (delay < 0.5)
                        {
                            delay = 0;
                            gd.StartTurn();
                            miss = false;
                            delay = 1;
                        }
                        else
                        {
                            delay = Mathf.Lerp(delay, 0, 1 * Time.deltaTime);
                        }
                    }
                }
                else
                {
                    if (attacking && gd.slider.value > gd.tempEnemyHealth)
                    {
                        if (gd.slider.value < gd.tempEnemyHealth + 1)
                        {
                            gd.slider.value = gd.tempEnemyHealth;
                            gd.StartTurn();
                        }
                        else
                        {
                            gd.slider.value = Mathf.Lerp(gd.slider.value, gd.tempEnemyHealth, 5 * Time.deltaTime);
                        }
                    }
                    else if (gd.slider.value == gd.tempEnemyHealth)
                    {
                        attacking = false;
                    }
                }
            }
        }
        else
        {
            if (delay > 0)
            {
                if (delay < 0.5)
                {
                    delay = 1;
                    gd.StartCoroutine("RollTheDice", true);
                    init = false;
                }
                else
                {
                    delay = Mathf.Lerp(delay, 0, 1 * Time.deltaTime);
                }
            }
        }
    }
}
