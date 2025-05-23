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
        EventBus.Subscribe("DamageEvent", HpUpdate);
        EventBus.Subscribe("EatEvent", Eat);
    }

    private void OnDisable()
    {
        EventBus.Subscribe("DamageEvent", HpUpdate);
    }

    public void HpUpdate(object evt)
    {
        //옮겨줘야할듯?
        stat.curHp -= (int)evt;
        if(stat.curHp < 0 )
        {
            stat.curHp = 0;
        }

        else if(stat.curHp > stat.maxHp)
        {
            stat.curHp = stat.maxHp;
        }
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
