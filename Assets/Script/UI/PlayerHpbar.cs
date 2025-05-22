using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpbar : MonoBehaviour
{
    public BaseStat stat;
    public Image hpBar;

    private void Awake()
    {
        stat = PlayerController.Instance.GetComponent<BaseStat>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<OnDamage>(HpUpdate);
        EventBus.Subscribe("DamageEvent", HpUpdate);
        EventBus.Subscribe("EatEvent", Eat);
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
        //옮겨줘야할듯?
        stat.curHp -= (int)evt;
        hpBar.fillAmount = (float)stat.curHp / (float)stat.maxHp;
    }

    public void HungerUpdate(object evt)
    {
        //
    }
    public void Eat(object evt)
    {
        ItemData item = (ItemData)evt;
        HpUpdate(- item.healValue);
        HungerUpdate(evt);
    }
}
