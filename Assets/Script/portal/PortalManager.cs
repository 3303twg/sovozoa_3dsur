using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    static public GameObject BluePortal;
    static public GameObject OrangePortal;



    private void Awake()
    {
        EventBus.Subscribe("Blue_PortalEvent", SetPortal);
        EventBus.Subscribe("Orange_PortalEvent", SetPortal);
        EventBus.Subscribe("PortalCreateEvent", SetPortal);

    }


    void SetPortal(object obj)
    {
        GameObject go = (GameObject)obj;

        if (go.name == "Blue_Portal")
        {
            if (BluePortal == null)
            {
                BluePortal = go;
            }
            else
            {
                BluePortal = go;
                EventBus.Publish("PortalDestroyEvent", go);
            }
        }
        else
        {
            if (OrangePortal == null)
            {
                OrangePortal = go;
            }
            else
            {
                OrangePortal = go;
                EventBus.Publish("PortalDestroyEvent", go);
            }
        }
    }
}
