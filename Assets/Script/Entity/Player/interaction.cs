using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour
{

    int damage = 10;


    public void OnDamageClass()
    {
        EventBus.Publish(new OnDamage(damage));
    }

    public void OnDamageString()
    {
        EventBus.Publish("DamageEvent", 20);
    }

}

public class OnDamage
{
    public int damage;

    public OnDamage(int damageValue)
    {
        damage = damageValue;
    }

}
