using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PortalBullet : MonoBehaviour
{
    public GameObject portal;


    public float speed = 1f;
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Debug.DrawRay(transform.position, transform.forward * 5f, Color.red, 2f);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            float dotResult = 0f;
            ContactPoint lastContact = new ContactPoint();

            foreach (ContactPoint contact in collision.contacts)
            {
                dotResult = Vector3.Dot(transform.forward, contact.normal);
                lastContact = contact;

            }
            float angle = Mathf.Acos(dotResult / (transform.forward.magnitude * lastContact.normal.magnitude)) * Mathf.Rad2Deg;
            Vector3 rotate = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + angle, transform.eulerAngles.z);
            GameObject go = Instantiate(portal, transform.position, Quaternion.Euler(rotate.x, rotate.y, rotate.z));

            //소환 후 이벤트 발송
            go.name = go.name.Replace("(Clone)", "");
            EventBus.Publish("PortalCreateEvent", go);
            Destroy(gameObject);
        }
    }
    */
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


}
