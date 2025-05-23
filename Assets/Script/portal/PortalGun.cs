using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour , IItemEffect
{
    public GameObject Blue;
    public GameObject Orange;

    private Camera playerCamera;

    [Header("���� ����")]
    public float maxDistance = 100f;
    public LayerMask hitLayers;


    private void Awake()
    {
        
    }
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0)) // ��Ŭ��
        {
            //ShootRay();
            LeftEffect();
            
        }

        if (Input.GetMouseButtonDown(1)) // ��Ŭ��
        {
            //ShootRay();
            RightEffect();
            
        }
        */
    }

    void ShootRay()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, hitLayers))
        {

        }
        else
        {

        }
    }

    //���ư��� ��
    public float shootForce = 10f;
    void ShootBluePortal()
    {
        GameObject projectile = Instantiate(Blue, playerCamera.transform.position, playerCamera.transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        //rb.velocity = playerCamera.transform.forward * shootForce;

        // ���⸸ ���� ���߱�
        //projectile.transform.forward = playerCamera.transform.forward;


    }
    void ShootOrangePortal()
    {
        GameObject projectile = Instantiate(Orange, playerCamera.transform.position, playerCamera.transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = playerCamera.transform.forward * shootForce;
        
    }
    
    public void LeftEffect()
    {
        playerCamera = Camera.main;
        //EventBus.Subscribe("UseLeftItemEvent", ShootBluePortal);
        ShootBluePortal();
    }

    public void RightEffect()
    {
        playerCamera = Camera.main;
        //EventBus.Subscribe("UseRightItemEvent", ShootOrangePortal);
        ShootOrangePortal();
    }
    
    /*
    public override void LeftEffect()
    {
        base.LeftEffect();
        Debug.Log("test");
        //1��ȿ��
    }
    */
}
















