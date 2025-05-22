using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;





public class PlayerController : Singleton<PlayerController>
{
    public BaseStat stat;
    public float mouseSensitivity = 100f;

    float xRotation = 0f;

    private Rigidbody rigidbody;
    Camera camera;

    public float maxRayDistance = 10f;


    public ItemData data;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        stat = GetComponent<BaseStat>();
        camera = Camera.main;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame


    private void FixedUpdate()
    {
        Move();
        
        Rotate();
    }

    void Update()
    {


        Jump();

        //Raycast();
        SelectItem();
        UseItem();
        DropItem();
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

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(Vector3.up * stat.jumpPower, ForceMode.Impulse);
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
    void SelectItem()
    {
        var a = Input.inputString;
        switch (a)
        {
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":

                EventBus.Publish("SelectItemEvent", int.Parse(a) - 1);
                break;

        }
    }

    void DropItem()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EventBus.Publish("PutDown", null);
        }
    }

    void UseItem()
    {
        /*
        Debug.Log("우클릭 아이템 사용 잠깐 껏음");
        
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("??");
            EventBus.Publish("UseItemEvent", null);
        }
        */
    }
}
