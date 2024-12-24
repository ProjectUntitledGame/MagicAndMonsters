using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell 
{
    public string name;
    public int power;
    public int element;
    public int hitRequired;
    public int d4, d6, d8;
    public Spell(string newName, int newElement, int hitReq, int newD4, int newD6, int newD8)
    {
        name = newName;
        hitRequired = hitReq;
        element = newElement;
        d4 = newD4;
        d6 = newD6;
        d8 = newD8;
    }
}
