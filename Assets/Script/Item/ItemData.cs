using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public string name;

    public bool stackable;

    public int maxStack;
    public int curStack;

    public int damage;
    public bool isMining;

    public bool isEating;
    public int fullness;

    public bool isHealing;
    public int healValue;



}
