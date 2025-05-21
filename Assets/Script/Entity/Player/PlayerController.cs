using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;





public class PlayerController : Singleton<PlayerController>
{
    public BaseStat stat;
    public float mouseSensitivity = 100f;

    float xRotation = 0f;

    Camera camera;

    public float maxRayDistance = 10f;

    private void Awake()
    {
        stat = GetComponent<BaseStat>();
        camera = Camera.main;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        Move();
        Rotate();
        Raycast();
    }


    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * stat.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * stat.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * stat.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * stat.speed * Time.deltaTime;
        }
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ���Ʒ� ȸ�� ����

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // ī�޶� ���Ʒ� ȸ��
        transform.Rotate(Vector3.up * mouseX);
    }
    void Raycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2 , 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayDistance))  // �ִ� �Ÿ� 100
        {
            Debug.Log("����ĳ��Ʈ�� ���� ������Ʈ: " + hit.collider.gameObject.name);

            // ���� ������Ʈ�� ���� �߰� ó�� ����
            // ��: hit.collider.gameObject.GetComponent<YourScript>() ...
        }
    }
}
