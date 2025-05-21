using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData data;
    public int index;

    public Image icon;
    public Text stackText;
    






    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = data.icon; // ? ��������Ʈ�� �ؾ��ҰŰ�����
        stackText.text = data.curStack > 1 ? data.curStack.ToString() : string.Empty;

    }

    public void Clear()
    {
        data = null;
        icon.gameObject.SetActive(false);
        stackText.text = string.Empty;
    }
}
