using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [HideInInspector]
    public Transform player; //ī�޶�������
    //[HideInInspector]
    public Transform thisPortal;     // ���� ��Ż (ī�޶� �ٶ󺸴� ��Ż)
    public Transform otherPortal;    // ����� ��Ż ��𼱰� ����ؾ��ҵ�
    private Camera portalCamera;



    public float fovHeight;
    public float fovWidth;
    public float fov = 60;
    //public float 


    private void Awake()
    {
        thisPortal = gameObject.transform.root;
        portalCamera = GetComponent<Camera>();

        EventBus.Subscribe("PortalCreateEvent", InitPortal);
        StartCoroutine(PortalDestroyEventCoroutine());


    }

    IEnumerator PortalDestroyEventCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        EventBus.Subscribe("PortalDestroyEvent", DestroyPortal);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe("PortalCreateEvent", InitPortal);
        EventBus.Unsubscribe("PortalDestroyEvent", DestroyPortal);
    }
    void Start()
    {
        player = Camera.main.transform;
        //���߿� �����Ǵ� Ÿ�ֿ̹� �����������
        portalCamera.transform.rotation = thisPortal.transform.rotation;

        // �ƿ� ���� �̰� ���Ϸ��� �˲����ߴµ�
        /*
        if (transform.root.gameObject.name == "Blue_Portal")
        {
            if (PortalManager.OrangePortal != null)
            {
                otherPortal = PortalManager.OrangePortal.transform;
            }
        }
        else if (transform.root.gameObject.name == "Orange_Portal")
        {
            if (PortalManager.BluePortal != null)
            {
                otherPortal = PortalManager.BluePortal.transform;
            }
        }
        */

    }

    public void InitPortal(object obj)
    {
        Debug.Log("��Ż �ʱ�ȭ �̺�Ʈ�� ����");
        GameObject go = (GameObject)obj;

        if (go.name != transform.root.name)
        {
            otherPortal = go.transform;
        }
    }

    public void DestroyPortal(object obj)
    {
        GameObject go = (GameObject)obj;
        if (go.name == transform.root.gameObject.name)
        {
            Destroy(transform.root.gameObject);
        }
    }

    void LateUpdate()
    {
        if (otherPortal != null)
        {


            UpdatePortalCameraPosition();
            AdjustCameraFrustum();
        }

    }

    void UpdatePortalCameraPosition()
    {
        
        // 1. �÷��̾��� ��ġ�� otherPortal �������� ���� ��ȯ
        Vector3 playerOffsetFromPortal = otherPortal.InverseTransformPoint(player.position);

        // 2. ��Ī ó�� (��: X�� ��Ī)
        Vector3 reflectedOffset = new Vector3(-playerOffsetFromPortal.x, playerOffsetFromPortal.y, -playerOffsetFromPortal.z);

        // 3. ȸ�� �����ؼ� newCamPosition ���
        Quaternion reflectedRotation = Quaternion.Inverse(thisPortal.rotation) * otherPortal.rotation;
        Vector3 newCamPosition = thisPortal.TransformPoint(reflectedOffset);

        transform.position = newCamPosition;




        /*
        // �÷��̾� ��ġ�� otherPortal ������ ���� ��ġ�� ��ȯ
        Vector3 localPos = otherPortal.InverseTransformPoint(player.position);

        // 180�� ȸ��(�յ� ����)
        Quaternion rot180 = Quaternion.Euler(0f, 180f, 0f);
        localPos = rot180 * localPos;

        // �ٽ� thisPortal �������� ��ȯ
        Vector3 newCamPos = thisPortal.TransformPoint(localPos);
        transform.position = newCamPos;

        // ȸ���� ��Ȯ�� �ݿ�
        Quaternion playerRotInOther = Quaternion.Inverse(otherPortal.rotation) * player.rotation;
        Quaternion newRot = thisPortal.rotation * rot180 * playerRotInOther;
        transform.rotation = newRot;
        */
    }


    /*
    
    void AdjustCameraFrustum()
    {
        Renderer targetRenderer = thisPortal.GetComponent<Renderer>();
        if (targetRenderer == null)
        {
            Debug.LogError("Target object must have a Renderer component.");
            return;
        }

        Bounds bounds = targetRenderer.bounds;
        Vector3 targetCenter = bounds.center;

        // ī�޶� �������� ��ȯ
        Matrix4x4 worldToCamera = portalCamera.worldToCameraMatrix;
        Vector3 localCenter = worldToCamera.MultiplyPoint(targetCenter);

        Vector3 localExtents = bounds.extents;
        Vector3 rightOffset = worldToCamera.MultiplyVector(Vector3.right * localExtents.x);
        Vector3 upOffset = worldToCamera.MultiplyVector(Vector3.up * localExtents.y);

        float left = localCenter.x - Mathf.Abs(rightOffset.x);
        float right = localCenter.x + Mathf.Abs(rightOffset.x);
        float bottom = localCenter.y - Mathf.Abs(upOffset.y);
        float top = localCenter.y + Mathf.Abs(upOffset.y);


        Vector3 dirToTarget = targetCenter - portalCamera.transform.position;

        // ī�޶��� ���� ����� �߽� ������ �Ÿ� (ī�޶� ���� Z)
        float distanceToCenter = Vector3.Dot(dirToTarget, portalCamera.transform.forward);

        // near�� �߽� - extents, far�� �߽� + extents
        float near = Mathf.Max(distanceToCenter - bounds.extents.z, 0.01f);
        //float far = distanceToCenter + bounds.extents.z + 0.01f;
        float far = Mathf.Max(distanceToCenter + bounds.extents.z + 0.01f, 100f);


        // ���� Projection Matrix ����
        Matrix4x4 proj = PerspectiveOffCenter(left, right, bottom, top, near, far);
        portalCamera.projectionMatrix = proj;

    }*/
    void AdjustCameraFrustum()
    {
        MeshFilter meshFilter = thisPortal.GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter�� �ʿ��մϴ�.");
            return;
        }

        Mesh mesh = meshFilter.sharedMesh;
        if (mesh == null)
        {
            Debug.LogError("Mesh�� �����ϴ�.");
            return;
        }

        Bounds localBounds = mesh.bounds; // ���� ����
        Vector3[] localCorners = new Vector3[8];
        Vector3 center = localBounds.center;
        Vector3 extents = localBounds.extents;

        // ���� ���� ������ ���
        localCorners[0] = center + new Vector3(+extents.x, +extents.y, +extents.z);
        localCorners[1] = center + new Vector3(+extents.x, +extents.y, -extents.z);
        localCorners[2] = center + new Vector3(+extents.x, -extents.y, +extents.z);
        localCorners[3] = center + new Vector3(+extents.x, -extents.y, -extents.z);
        localCorners[4] = center + new Vector3(-extents.x, +extents.y, +extents.z);
        localCorners[5] = center + new Vector3(-extents.x, +extents.y, -extents.z);
        localCorners[6] = center + new Vector3(-extents.x, -extents.y, +extents.z);
        localCorners[7] = center + new Vector3(-extents.x, -extents.y, -extents.z);

        // ���� �� ���� �� ī�޶� ���� ��ȯ
        Matrix4x4 localToWorld = meshFilter.transform.localToWorldMatrix;
        Matrix4x4 worldToCamera = portalCamera.worldToCameraMatrix;

        Vector3[] cameraPoints = new Vector3[8];
        for (int i = 0; i < 8; i++)
        {
            Vector3 world = localToWorld.MultiplyPoint(localCorners[i]);
            cameraPoints[i] = worldToCamera.MultiplyPoint(world);
        }

        float left = float.MaxValue;
        float right = float.MinValue;
        float bottom = float.MaxValue;
        float top = float.MinValue;
        float near = float.MaxValue;
        float far = float.MinValue;

        foreach (Vector3 p in cameraPoints)
        {
            left = Mathf.Min(left, p.x);
            right = Mathf.Max(right, p.x);
            bottom = Mathf.Min(bottom, p.y);
            top = Mathf.Max(top, p.y);
            near = Mathf.Min(near, p.z);
            far = Mathf.Max(far, p.z);
        }

        near = Mathf.Max(-far, 0.01f);
        far = Mathf.Max(-near + 0.1f, near + 0.1f);

        Matrix4x4 proj = PerspectiveOffCenter(left, right, bottom, top, near, 100f);
        portalCamera.projectionMatrix = proj;
    }


    Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
    {
        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = 2.0f * near / (right - left);
        m[0, 2] = (right + left) / (right - left);
        m[1, 1] = 2.0f * near / (top - bottom);
        m[1, 2] = (top + bottom) / (top - bottom);
        m[2, 2] = -(far + near) / (far - near);
        m[2, 3] = -(2.0f * far * near) / (far - near);
        m[3, 2] = -1.0f;
        m[3, 3] = 0.0f;
        return m;
    }
}