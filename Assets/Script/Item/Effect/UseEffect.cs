using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseEffect : MonoBehaviour, IItemEffect
{
    public int healValue = 10;
    public void LeftEffect()
    {

    }

    public void RightEffect()
    {
        Heal();
        EventBus.Publish("UseItemEvent", null);
    }



    public void Heal()
    {
        EventBus.Publish("DamageEvent", healValue);
    }
}
