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
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 위아래 회전 제한

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // 카메라 위아래 회전
        transform.Rotate(Vector3.up * mouseX);
    }
    void Raycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2 , 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayDistance))  // 최대 거리 100
        {
            Debug.Log("레이캐스트가 맞은 오브젝트: " + hit.collider.gameObject.name);

            // 맞은 오브젝트에 대해 추가 처리 가능
            // 예: hit.collider.gameObject.GetComponent<YourScript>() ...
        }
    }
}
