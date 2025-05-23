using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public interface IItemEffect 
{
    void LeftEffect();
    void RightEffect();
}

public class Item : MonoBehaviour
{
    public ItemData data;
    public GameObject canvas;
    public Text name;

    public IItemEffect effect;

    private void Awake()
    {
        name.text = data.name;
        //effect = GetComponent<IItemEffect>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventBus.Publish("PickUp", gameObject);
            Destroy(gameObject.transform.root.gameObject);
        }
    }

    /*
    public void UseLeftItem()
    {
        EventBus.Publish("UseLeftItemEvent", null);
    }
    public void UseRightItem()
    {
        EventBus.Publish("UseRightItemEvent", null);
    }
    public void UseItem()
    {
        //EventBus.Publish("UseItemEvent", null);
    }
    public void UseLeft()
    {
        effect?.LeftEffect();
    }

    public void UseRight()
    {
        effect?.RightEffect();
    }
    */
    /*
    public virtual void LeftEffect()
    {
        Debug.Log("¾È´ï");
        //effect = GetComponent<IItemEffect>();
    }

    public virtual void RightEffect()
    {

    }
    */

}
