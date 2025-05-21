using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public GameObject canvas;
    public Text name;

    private void Awake()
    {
        name.text = data.name;
        EventBus.Subscribe("dropItem", ShowName);
    }


    public void ShowName(object evt)
    {
        //Èì.. ¸À¾ø´Âµ¥?
        EventBus.Unsubscribe("dropItem",ShowName);
        canvas.SetActive(true);
        EventBus.Subscribe("dropItem", HideName);
    }

    public void HideName(object evt)
    {
        canvas.SetActive(false);
    }
}
