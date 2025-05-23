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

    //�ٴ� ������
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
        //���콺 ��ġ ������ �� �����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //�� ����?
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
        //üũ��
        isGround = IsGrounded();
        Jump();
        //Raycast();
        SelectItem();
        UseItem();
        DropItem();
    }


    private void Move()
    {
        //�밢���� ����ȭ �ؾ��ҵ�?
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
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ���Ʒ� ȸ�� ����

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // ī�޶� ���Ʒ� ȸ��
        transform.Rotate(Vector3.up * mouseX);
    }
    

    //������ �ֹ�
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


    //������ ������
    void DropItem()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EventBus.Publish("PutDown", null);
        }
    }

    //������ ��� ���� ����
    void UseItem()
    {
        if (item != null)
        {
            if (item.GetComponent<IItemEffect>() != null)
            {
                if (Input.GetMouseButtonDown(0)) // ��Ŭ��
                {
                    //IItemEffect usable = item.GetComponent<IItemEffect>();

                    item.GetComponent<IItemEffect>().LeftEffect();
                    //item.LeftEffect();
                    //item.GetComponent<PortalGun>().LeftEffect();
                }

                if (Input.GetMouseButtonDown(1)) // ��Ŭ��
                {
                    item.GetComponent<IItemEffect>().RightEffect();
                }
            }
        }
        
        /*
        Debug.Log("��Ŭ�� ������ ��� ��� ����");
        
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("??");
            EventBus.Publish("UseItemEvent", null);
        }
        */
    }
}
