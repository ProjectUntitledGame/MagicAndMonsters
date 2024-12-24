using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSave : MonoBehaviour
{
    public string spellName;
    private SpellBook spellBook;
    public int spellSlotIndex;
    public Text spellNameTxt;
    public void SelectSpell()
    {
        
        spellBook.tempSlot = this;
        spellBook.UpdateSlots();
        
    }
    public string tempSpellString;
    public void DeleteSpell()
    {
        PlayerPrefs.SetString("SpellBook" + spellSlotIndex, spellBook.gameObject.GetComponent<GlobalData>().newSpellHolder);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("GlobalData").GetComponent<SpellBook>().spellBookSlots[spellSlotIndex] = this;
        if (spellSlotIndex == 0 && PlayerPrefs.GetString("SpellBook" + spellSlotIndex) == "")
        {
            PlayerPrefs.SetString("SpellBook" + 0, "Spark");
        }
        
        spellName = PlayerPrefs.GetString("SpellBook" + spellSlotIndex);
        spellNameTxt.text = spellName;
        spellBook = this.gameObject.GetComponent<SpellBook>();
        if(spellBook == null)
        {
            spellBook = GameObject.FindGameObjectWithTag("GlobalData").GetComponent<SpellBook>();
        }
        

    }

   
}
