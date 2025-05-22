using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{

    public GameObject other_portal;

    void Start()
    {
        
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        PortalBullet bulletScript = other.gameObject.GetComponent<PortalBullet>();
        if (bulletScript == null) return;

        else
        {
            if (other.gameObject.GetComponent<PortalBullet>().isExit == true)
            {
                Debug.Log("µø¿€«‘");
                other.gameObject.GetComponent<PortalBullet>().isExit = false;
                other.gameObject.transform.position = other_portal.transform.position;
            }

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        bullet bulletScript = other.gameObject.GetComponent<bullet>();
        if (bulletScript == null) return;

        else
        {
            other.gameObject.GetComponent<bullet>().chang_state_call();
        }
    }
    */

}
