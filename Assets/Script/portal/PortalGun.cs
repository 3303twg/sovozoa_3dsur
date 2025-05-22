using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public GameObject Blue;
    public GameObject Orange;

    [Header("카메라 참조")]
    public Camera playerCamera;

    [Header("레이 설정")]
    public float maxDistance = 100f;
    public LayerMask hitLayers;


    private void Awake()
    {
        playerCamera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 좌클릭
        {
            //ShootRay();
            ShootBluePortal();
        }

        if (Input.GetMouseButtonDown(1)) // 우클릭
        {
            //ShootRay();
            ShootOrangePortal();
        }
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
}
















