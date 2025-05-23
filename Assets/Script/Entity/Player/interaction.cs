using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interaction : MonoBehaviour
{
    public float maxRayDistance = 10f;
    public Text itemName;


    private void Update()
    {
        CheckItem();
    }
    void CheckItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayDistance))  // 최대 거리 100
        {
            if (hit.collider.gameObject.GetComponent<Item>() != null)
            {
                itemName.gameObject.SetActive(true);
                itemName.text = hit.collider.gameObject.GetComponent<Item>().data.name;
            }
            else
            {
                itemName.gameObject.SetActive(false);
            }
        }
        else
        {
            
            itemName.text = null;
        }
    }

}
