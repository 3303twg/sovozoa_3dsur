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
        //�߻�ü ���� �̵�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Debug.DrawRay(transform.position, transform.forward * 5f, Color.red, 2f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        //�÷��̾���ġ�����̶� �÷��̾� ��ġ�°� ����
        if(!collision.gameObject.CompareTag("Player"))
        //if (!other.CompareTag("Player"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1.0f))
            {
                //�浹�� ������Ʈ�� �浹ü���� �ٶ󺸰���
                Quaternion rot = Quaternion.LookRotation(hit.normal);
                Vector3 rotate = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, rot.eulerAngles.z);

                //��ȯ �� �̺�Ʈ �߼�
                GameObject go = Instantiate(portal, transform.position, Quaternion.Euler(rotate.x, rotate.y, rotate.z));
                go.name = go.name.Replace("(Clone)", "");
                EventBus.Publish("PortalCreateEvent", go);

                //�浹ü �ı�
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
