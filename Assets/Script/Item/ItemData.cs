using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string name;

    public GameObject prefab;
    public Sprite icon;

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
