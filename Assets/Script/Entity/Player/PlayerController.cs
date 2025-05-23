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

    public Item item;


    //test
    [LayerName]
    public string LayerList;

    [SerializeField] private LayerMask groundMask;


    public bool isGround;

    //바닥 감지용
    bool IsGrounded()
    {
        Vector3[] offsets = new Vector3[]
        {
        transform.forward * 0.2f,
        -transform.forward * 0.2f,
        transform.right * 0.2f,
        -transform.right * 0.2f
        };

        foreach (var offset in offsets)
        {
            Vector3 origin = transform.position + offset;
            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 1.2f, groundMask))
            {
                Debug.DrawRay(origin, Vector3.down * 1.2f, Color.green);
                return true;
            }
            else
            {
                Debug.DrawRay(origin, Vector3.down * 1.2f, Color.red);
            }
        }

        return false;
    }
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        stat = GetComponent<BaseStat>();
        camera = Camera.main;
    }
    void Start()
    {
        //마우스 위치 고정용 및 숨기기
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //너 뭐냐?
        /*
        EventBus.Publish("LeftEffectEvent", null);
        EventBus.Publish("RightEffectEvent", null);
        */
    }

    // Update is called once per frame


    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Update()
    {
        //체크용
        isGround = IsGrounded();
        Jump();
        //Raycast();
        SelectItem();
        UseItem();
        DropItem();
    }


    private void Move()
    {
        //대각방향 정규화 해야할듯?
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
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
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
    

    //퀵슬롯 핫바
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


    //아이템 버리기
    void DropItem()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EventBus.Publish("PutDown", null);
        }
    }

    //아이템 사용 공격 포함
    void UseItem()
    {
        if (item != null)
        {
            if (item.GetComponent<IItemEffect>() != null)
            {
                if (Input.GetMouseButtonDown(0)) // 좌클릭
                {
                    //IItemEffect usable = item.GetComponent<IItemEffect>();

                    item.GetComponent<IItemEffect>().LeftEffect();
                    //item.LeftEffect();
                    //item.GetComponent<PortalGun>().LeftEffect();
                }

                if (Input.GetMouseButtonDown(1)) // 우클릭
                {
                    item.GetComponent<IItemEffect>().RightEffect();
                }
            }
        }
        
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
