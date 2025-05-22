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

    public int index = 0;
    public ItemData selectData;


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
        EventBus.Subscribe("SelectItemEvent", SelectItem);
        EventBus.Subscribe("UseItemEvent", UseItem);

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
                slot.data.curStack++;
                RefreshSlot();
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.data = data;
            emptySlot.data.curStack = 1;
            RefreshSlot();
            return;
        }
    }

    void SubtractItem()
    {
        slots[index].data.curStack -= 1;

        if (slots[index].data.curStack < 1)
        {
            data = null;
            slots[index].data = null;

        }
        RefreshSlot();
    }
    void DropItem(object obj)
    {
        

        if (slots[index].data == null) return;

        data = slots[index].data;


        Vector3 dropPos = PlayerController.Instance.transform.position + PlayerController.Instance.transform.forward * 1f;
        Instantiate(data.prefab, dropPos, Quaternion.identity);
        SubtractItem();
    }

    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].data == data && slots[i].data.curStack < data.maxStack)
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

    public void SelectItem(object obj)
    {
        //흠 그냥 인덱스만 들고있으면 되지않을까?
        index = (int)obj;
        
    }

    public void UseItem(object obj)
    {
        Debug.Log("?");
        //회복 후
        if(slots[index].data.isEating == true)
        {
            EventBus.Publish("EatEvent", slots[index].data);
        }
        SubtractItem();
    }
}

