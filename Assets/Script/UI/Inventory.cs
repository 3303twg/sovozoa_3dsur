using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public ItemData data;

    public GameObject slotPanel;

    private void Awake()
    {
        slots = new ItemSlot[slotPanel.transform.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.transform.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            //slots[i].inventory = this;
        }


        EventBus.Subscribe("PickUp", AddItem);
        EventBus.Subscribe("PutDown", DropItem);

    }



    void AddItem(object obj)
    {
        GameObject go = obj as GameObject;
        data = go.GetComponent<Item>().data;

        if (data.stackable == true)
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.stack++;
                RefreshSlot();
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.data = data;
            emptySlot.stack = 1;
            RefreshSlot();
            return;
        }
    }
    void DropItem(object obj)
    {

    }

    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].data == data && slots[i].stack < data.maxStack)
            {
                return slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].data == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    void RefreshSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].data != null)
            {
                slots[i].Set();
            }

            else
            {
                slots[i].Clear();
            }
        }
    }
}

