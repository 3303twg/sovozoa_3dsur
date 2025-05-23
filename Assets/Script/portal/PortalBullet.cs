using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PortalBullet : MonoBehaviour
{
    public GameObject portal;


    public float speed = 1f;
    private void FixedUpdate()
    {
        //발사체 정면 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Debug.DrawRay(transform.position, transform.forward * 5f, Color.red, 2f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        //플레이어위치스폰이라 플레이어 겹치는거 방지
        if(!collision.gameObject.CompareTag("Player"))
        //if (!other.CompareTag("Player"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1.0f))
            {
                //충돌한 오브젝트를 충돌체에게 바라보게함
                Quaternion rot = Quaternion.LookRotation(hit.normal);
                Vector3 rotate = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, rot.eulerAngles.z);

                //소환 후 이벤트 발송
                GameObject go = Instantiate(portal, transform.position, Quaternion.Euler(rotate.x, rotate.y, rotate.z));
                go.name = go.name.Replace("(Clone)", "");
                EventBus.Publish("PortalCreateEvent", go);

                //충돌체 파괴
                Destroy(gameObject);
            }
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Vector3 bulletDirection = transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, bulletDirection, out hit, 1.0f))
            {
                Quaternion rot = Quaternion.LookRotation(hit.normal);

                Vector3 rotate = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, rot.eulerAngles.z); 

                GameObject go = Instantiate(portal, transform.position, Quaternion.Euler(rotate.x, rotate.y, rotate.z));
                go.name = go.name.Replace("(Clone)", "");
                EventBus.Publish("PortalCreateEvent", go);

                Destroy(gameObject);
            }
        }
    }

    */


}
