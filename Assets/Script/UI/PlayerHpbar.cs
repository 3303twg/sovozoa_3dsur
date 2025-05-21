using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpbar : MonoBehaviour
{
    public BaseStat stat;


    private void Awake()
    {
        stat = PlayerController.Instance.GetComponent<BaseStat>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnDamage>(HpUpdate);
        EventBus.Subscribe("DamageEvent", HpUpdate);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnDamage>(HpUpdate);
        EventBus.Subscribe("DamageEvent", HpUpdate);
    }
    public void HpUpdate(OnDamage evt)
    {
        stat.curHp -= evt.damage;
    }

    public void HpUpdate(object evt)
    {
        stat.curHp -= (int)evt;
    }

}
