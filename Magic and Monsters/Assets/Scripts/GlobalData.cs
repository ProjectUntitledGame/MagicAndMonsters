using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalData : MonoBehaviour
{
    //THIS IS THE HUB AREA
    [Header("UI")]
    public GameObject enemyInfo;
    public MenuController holdCurrentMenu;
    public Text enemyName;
    public int enemyHealth;
    public Sprite[] enemySprites;
    public Sprite[] enemyKOSprites;
    public int spriteIndex;
    public Text levelName;
    public GameObject spellSheet;
    public Image[] hubEnemies;
    public GameObject blackScreen;
    public GameObject enemyTab;
    public GameObject[] temp;

    public void OpenEnemyTab()
    {
        enemyTab.SetActive(enemyTab);
    }


    public void OpenSpell()
    {

        spellSheet.SetActive(true);
    }

    public GameObject settings;

    public void OpenSettings()
    {
        settings.SetActive(true);
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }

    private bool menuOpen;
    public int level;

    public void OpenInfo()
    {
        if (!menuOpen)
        {
            enemyInfo.SetActive(true);
            enemyName.text = holdCurrentMenu.enemyName; 
            spriteIndex = holdCurrentMenu.sIndex;
            level = holdCurrentMenu.level;
            for(int i = 0; i < enemySpells.Length; i++)
            {
                enemySpells[i] = holdCurrentMenu.spells[i];
            }
            tempName = holdCurrentMenu.enemyName;
            enemyHealth = holdCurrentMenu.enemyHealth;
            menuOpen = true;
        }
    }

    public GameObject levelPanel;
    public void OpenLevelSelect()
    {
        levelPanel.SetActive(true);
    }

    public GameObject shop;
    public void OpenShop()
    {
        shop.SetActive(true);
    }
    public GameObject holdPanel;
    
    public void CloseMenu()
    {
        holdPanel.SetActive(false);
        menuOpen = false;
    }
    public int sceneIndex;
    public void LoadLevel()
    {
        fadeInOrOut = true;
        StartCoroutine("FadeToBlack");
    }
    private float fadeSpeed = 5;
    private float fadeAmount;
    private bool fadeInOrOut;
    IEnumerator FadeToBlack()
    {
        blackScreen.SetActive(true);
        Color screenColour = blackScreen.GetComponent<Image>().color;
        if (fadeInOrOut)
        {
            while (screenColour.a < 1)
            {
                fadeAmount = screenColour.a + (fadeSpeed * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeAmount);
                blackScreen.GetComponent<Image>().color = screenColour;
                yield return null;
            }
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            while (screenColour.a > 0)
            {
                fadeAmount = screenColour.a - (fadeSpeed * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeAmount);
                blackScreen.GetComponent<Image>().color = screenColour;
                yield return null;
            }
            blackScreen.SetActive(false);
        }
    }
    
    public void OpenCombat()
    {
        PlayerPrefs.SetInt("sIndex", spriteIndex);
        PlayerPrefs.SetInt("tempHealth", enemyHealth);
        PlayerPrefs.SetString("tempName", tempName);
        PlayerPrefs.SetString("tempLevelName", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("tempLevelInt", level);
        for (int i = 0; i < enemySpells.Length; i++)
        {
            PlayerPrefs.SetString("ESpell " + i.ToString(), enemySpells[i]);

        }

        
        SceneManager.LoadScene(1);
    }
    
    //THIS IS THE COMBAT AREA
    [Header("Combat")]
    public Text roll;
    public Slider slider;
    public string[] enemySpells;
    public GameObject winScreen;
    public int tempEnemyHealth;
    public string tempName;
    public SpellBook spells;
    public Image enemyImage;
    public float playerHealth = 500;
    public Slider playerBar;
    public GameObject scrollAnim;
    public int levelNum;
    public Text spellDisplay;
    public Text hitRequired;
    public Text damageDie;
    public Text eSpellDisplay;
    public Text eHitRequired;
    public Text eDamageDie;
    public GameObject psInfo;
    public GameObject esInfo;
    private float healthMulti;
    public MenuController initCont;

    private void Start()
    {
        healthMulti = playerHealth / 100;
        if(SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1)
        {
            PlayerPrefs.SetInt("CurrentIndex", SceneManager.GetActiveScene().buildIndex);
        }
        turn = false;
        fadeInOrOut = false;
        StartCoroutine("FadeToBlack");
        if(levelName != null)
        {
            levelName.text = SceneManager.GetActiveScene().name;
            for(int i = 0; i <= (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name) -1); i++)
            {
                hubEnemies[i].sprite = enemyKOSprites[i];
            }
        }
        if (slider != null)
        {
            level = PlayerPrefs.GetInt("tempLevelInt");
            spriteIndex = PlayerPrefs.GetInt("sIndex");
            enemyImage.sprite = enemySprites[spriteIndex];
            tempEnemyHealth = PlayerPrefs.GetInt("tempHealth");
            tempName = PlayerPrefs.GetString("tempName");
            for(int i = 0; i < enemySpells.Length; i++)
            {
                enemySpells[i] = PlayerPrefs.GetString("ESpell " + i.ToString());
                
            }
            slider.maxValue = tempEnemyHealth;
            slider.value = tempEnemyHealth;
        }
    }
    public void Return()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentIndex"));
    }


    public bool turn = true;
    
    public void StartTurn()
    {
        StartCoroutine("MovePanel", esInfo);
    }
    public Text eResult;
    public Text displayResult;
    public int playerInit;
    public int enemyInit;
    public void EnemyTurn()
    {
        turnCount++;
        turnDisplay.text = "Their Turn";
        turnCountDisplay.text = "Turn " + turnCount;
        int i = Random.Range(0, 3);
        eSpellDisplay.text = spells.spells[enemySpells[i].ToString()].name;
        eHitRequired.text = "Roll to Hit: " + spells.spells[enemySpells[i].ToString()].hitRequired.ToString();
        displayResult = eResult;
        StartCoroutine("RollTheDice", false);
        hitOrMiss.text = "";
    }
    public void Result(int result, bool initiative)
    {
        if (!initiative)
        {
            if (result < 10)
            {
                hitOrMiss.text = " MISS!";
                miss = true;
                attacking = true;
            }
            else
            {
                attacking = true;
                hitOrMiss.text = " HIT!";
                playerHealth -= 10;
                playerHealth = playerHealth - spells.spells[spellDisplay.text].power;

            }
        }
        else
        {
            enemyInit = result;
            startGame = true;

        }
        
    }

    public Text initResult;
    public Sprite[] dice;
    public int finalSide;
    private IEnumerator RollTheDice(bool initiative)
    {
        int temp = 0;

        for (int i = 0; i <= 10; i++)
        {
            temp = Random.Range(0, 19);
            displayResult.text = temp.ToString();
            yield return new WaitForSeconds(0.05f);
        }
        int finalSide = temp + 1;
        displayResult.text = finalSide.ToString();
        Result(finalSide, initiative);
    }

    private bool miss;
    public bool enemyTurn;
    private float delay = 1f;
    private bool attacking = false;
    public GameObject defeatScreen;
    public Text turnDisplay;
    public Text turnCountDisplay;
    public Text hitOrMiss;
    public bool alive = true;
    private bool startGame;
    private int turnCount = 0;
    private void Update()
    {
        if(playerHealth <= 0)
        {
            defeatScreen.SetActive(true);
        }
        else
        {
            if (!startGame)
            {
                if (miss)
                {
                    if (delay > 0)
                    {
                        
                        if (delay < 0.5)
                        {
                            delay = 0;
                            StartCoroutine("MovePanel", psInfo);
                            hitOrMiss.text = "";
                            miss = false;
                            turnDisplay.text = "Your Turn";
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
                    if (attacking && playerBar.value > playerHealth)
                    {
                        
                        if (playerBar.value < playerHealth + 5)
                        {
                            playerBar.value = playerHealth;
                            if (playerHealth <= 5)
                            {
                                defeatScreen.SetActive(true);
                            }
                            else
                            {
                                
                                StartCoroutine("MovePanel", psInfo);
                                
                            }
                            turnDisplay.text = "Your Turn" ;

                            hitOrMiss.text = "";
                        }
                        else
                        {
                            
                            playerBar.value = Mathf.Lerp(playerBar.value, playerHealth, 10 * Time.deltaTime);
                        }
                    }
                    else if (playerBar != null && playerBar.value == playerHealth)
                    {
                        attacking = false;
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
                        startGame = false;
                        if (playerInit >= enemyInit)
                        {
                            turn = true;
                            StartCoroutine("MovePanel", psInfo);
                            turnCount++;
                            
                        }
                        else
                        {
                            StartTurn();
                           
                        }
                        startGame = false;
                        initCont.SetPanel();
                    }
                    else
                    {
                        delay = Mathf.Lerp(delay, 0, 1 * Time.deltaTime);
                    }
                }
            }
        }
    }
    public Transform eSpawn;
    public Transform pSpawn;
    public Transform target;
    public IEnumerator MovePanel(GameObject tempPanel)
    {
       // Vector3 target = new 
         
        if (tempPanel.tag == "Player")
        {

            while (psInfo.transform.position.x != target.position.x)
            {
                esInfo.transform.position = Vector3.MoveTowards(esInfo.transform.position, eSpawn.position, 1000 * Time.deltaTime);
                psInfo.transform.position = Vector3.MoveTowards(psInfo.transform.position, target.position, 1000 * Time.deltaTime);
                yield return new WaitForSeconds(0f);
            }
            turn = true;
        }
        else
        {
            while (esInfo.transform.position.x != target.position.x)
            {
                psInfo.transform.position = Vector3.MoveTowards(psInfo.transform.position, pSpawn.position, 1000 * Time.deltaTime);
                esInfo.transform.position = Vector3.MoveTowards(esInfo.transform.position, target.position, 1000 * Time.deltaTime);
                yield return new WaitForSeconds(0f);
            }

            EnemyTurn();
        }
    }

    public GameObject[] hitDie;
    public IEnumerator RollHit(string spell)
    {
        int temp = 0;
        int tempTotal = spells.spells[spell].d4 + spells.spells[spell].d6 + spells.spells[spell].d8;
        for(int i = 0; i < tempTotal; i++)
        {
            hitDie[i].SetActive(true);
        }
        for (int i = 0; i <= 10; i++)
        {
            temp = Random.Range(0, 19);
            displayResult.text = temp.ToString();
            yield return new WaitForSeconds(0.05f);
        }
        int finalSide = temp + 1;
        for (int i = 0; i < tempTotal; i++)
        {
            hitDie[i].SetActive(false);
        }
        displayResult.text = finalSide.ToString();
        
    }


    [Header("Reward")]
    public int element;
    public int reward;
    public Image elementImage;
    public Sprite[] elementImages;
    public string newSpellHolder;
    public void GetReward()
    {
        int temp = Random.Range(0, 4);
        elementImage.sprite = elementImages[spells.spells[enemySpells[temp].ToString()].element];
        for(int i = 0; i < spells.spellBookSlots.Length; i++)
        {
            if(spells.spellBookSlots[i].spellName == "")
            {
                PlayerPrefs.SetString("SpellBook" + i, spells.spells[enemySpells[temp].ToString()].ToString() );
                
                i = spells.spellBookSlots.Length;
            }
            else
            {
                newSpellHolder = enemySpells[temp].ToString();
                spellSheet.SetActive(true);
            }
        }

    }

    
}
