using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public BaseStat stat;
    public float mouseSensitivity = 100f;

    float xRotation = 0f;

    Camera camera;

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
        if(Input.GetKey(KeyCode.W))
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

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 위아래 회전 제한

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // 카메라 위아래 회전
        transform.Rotate(Vector3.up * mouseX);
    }
}
