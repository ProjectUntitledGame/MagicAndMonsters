using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    public string[] spellList;
    public SpellInfo[] spellSlots;
    public SpellSave[] spellBookSlots;
    public Dictionary<string, Spell> spells = new Dictionary<string, Spell>();
    public Spell temp = null;
    public int count = 0;
    public SpellInfo tempSpell;

    Spell sp1 = new Spell("Spark",      2,   10,      2, 0, 0);
    Spell sp2 = new Spell("Ignite",     0,   12,      0, 1, 0);
    Spell sp3 = new Spell("Rain",       1,   10,      3, 0, 0);
    Spell sp4 = new Spell("Shadow",     5,   12,      2, 0, 0);
    Spell sp5 = new Spell("Waterball",  1,   15,      0, 3, 0);
    Spell sp6 = new Spell("Lightning",  2,   15,      2, 2, 0);


    void Awake()
    {

        spells.Add("Spark", sp1);
        spells.Add("Ignite", sp2);
        spells.Add("Rain", sp3);
        spells.Add("Shadow", sp4);
        spells.Add("Waterball", sp5);
        spells.Add("Lightning", sp6);
        UpdateBook();
        //UpdateSlots();

    }
    public SpellSave tempSlot;
    //This is the spellbook slots.
    public void UpdateSlots()
    {
        tempSpell.spellName = spells[PlayerPrefs.GetString("SpellBook" + tempSlot.spellSlotIndex)].name;
        tempSpell.power = spells[PlayerPrefs.GetString("SpellBook" + tempSlot.spellSlotIndex)].power;
        tempSpell.text.text = tempSpell.spellName;
        PlayerPrefs.SetString("SpellSlot" + tempSpell.buttonIndex, tempSpell.spellName);
        //We now need to make the "Spellbook" playerpref. This is literally just saving the spell when it drops and then indexing it to the highest one without a spell saved. 'For' could be used to scan until it is set.
    }

    


    public void UpdateBook()
    {
        for (int i = 0; i < spellSlots.Length; i++)
        {
            if (PlayerPrefs.GetString("SpellSlot" + i) == "")
            {
                spellSlots[i].spellName = spells["Spark"].name;
                spellSlots[i].power = spells["Spark"].power;
                spellSlots[i].text.text = spellSlots[i].spellName;
            }
            else
            {
                spellSlots[i].spellName = spells[PlayerPrefs.GetString("SpellSlot" + i)].name;
                spellSlots[i].power = spells[PlayerPrefs.GetString("SpellSlot" + i)].power;
                spellSlots[i].text.text = spellSlots[i].spellName;
            }
            
            
        }
    }



}
