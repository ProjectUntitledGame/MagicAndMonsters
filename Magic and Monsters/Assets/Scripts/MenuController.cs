using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    
    public GameObject panel;
    GlobalData global;
    public void Start()
    {
        global = GameObject.FindGameObjectWithTag("GlobalData").GetComponent<GlobalData>();
    }
    public void SetPanel()
    {
        global.holdPanel = panel;
        global.CloseMenu();
    }

    public void SetInfo()
    {
        if(level <= PlayerPrefs.GetInt(SceneManager.GetActiveScene().name))
        {
            global.holdCurrentMenu = this;
            global.OpenInfo();
        }

    }

    public int sceneIndex;
    public void StartGame()
    {
        Debug.Log(sceneIndex = PlayerPrefs.GetInt("CurrentIndex"));
        if (PlayerPrefs.GetInt("CurrentIndex") != 0)
        {
            sceneIndex = PlayerPrefs.GetInt("CurrentIndex");
        }
        else
        {
            sceneIndex = 2;
        }
        
        SetIndex();
    }
    public void SetIndex()
    {
        global.sceneIndex = sceneIndex;
        global.LoadLevel();
    }

    public GameObject screen;
    public GameObject otherScreen;

    public void ChangeScreen()
    {
        screen.SetActive(true);
        otherScreen.SetActive(false); 
    }

    [Header("Combat")]
    public string enemyName;
    public int enemyHealth;
    public int sIndex;
    public string[] spells;

    [Header("HubWorld")]
    public int level;
    public GameObject levelLock;
    private void Awake()
    {
        //PlayerPrefs.DeleteKey(SceneManager.GetActiveScene().name);
        if(levelLock != null)
        {
            if (level <= PlayerPrefs.GetInt(SceneManager.GetActiveScene().name))
            {
                levelLock.SetActive(false);
            }
            
        }
    }

    [Header("Encyclopedia")]
    public GameObject page;
    public Image encyclopediaImage;
    public Image encyclopediaKOImage;
    public Text encyclopediaName;
    public Text encyclopediaDesc;
    public int sbSprite;
    public string sbName;
    public string sbDesc;

    public void OpenInfoPage()
    {
        encyclopediaImage.sprite = global.gameObject.GetComponent<ShopSave>().playerSprite[sbSprite];
        encyclopediaKOImage.sprite = global.gameObject.GetComponent<ShopSave>().playerKOSprite[sbSprite];
        encyclopediaName.text = sbName;
        encyclopediaDesc.text = sbDesc;
        page.SetActive(true);
    }

}
