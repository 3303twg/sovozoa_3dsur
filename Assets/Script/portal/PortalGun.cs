using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour , IItemEffect
{
    public GameObject Blue;
    public GameObject Orange;

    private Camera playerCamera;

    [Header("레이 설정")]
    public float maxDistance = 100f;
    public LayerMask hitLayers;


    private void Awake()
    {
        
    }
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0)) // 좌클릭
        {
            //ShootRay();
            LeftEffect();
            
        }

        if (Input.GetMouseButtonDown(1)) // 우클릭
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

    //날아가는 힘
    public float shootForce = 10f;
    void ShootBluePortal()
    {
        GameObject projectile = Instantiate(Blue, playerCamera.transform.position, playerCamera.transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        //rb.velocity = playerCamera.transform.forward * shootForce;

        // 방향만 따로 맞추기
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
        //1번효과
    }
    */
}
















