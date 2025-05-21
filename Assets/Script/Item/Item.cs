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

    }


    public void ShowName(object evt)
    {
        //Èì.. ¸À¾ø´Âµ¥?

        canvas.SetActive(true);

    }

    public void HideName(object evt)
    {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventBus.Publish("PickUp", gameObject);
            Destroy(gameObject);
        }
    }
}
