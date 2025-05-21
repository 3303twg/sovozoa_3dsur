using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAnimationCurve : MonoBehaviour
{
    public AnimationCurve speedCurve;  // �����Ϳ��� � ���� ���� ����
    public float duration = 3f;         // ������ ���� �ð�
    public Vector3 startPos;
    public Vector3 endPos;

    public float rotateSpeed = 10f;
    private float timer = 0f;


    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / duration;      // 0 ~ 1 ����
            float curveValue = speedCurve.Evaluate(normalizedTime);  // ��� ���� �ӵ��� ���

            // ���� �̵��� ��� ���ؼ� �ڿ������� �̵�
            transform.position = Vector3.Lerp(startPos, endPos, curveValue);
        }
        else
        {
            timer = 0f;
        }

        transform.rotation *= Quaternion.Euler(0, rotateSpeed * Time.deltaTime, 0);
    }
}
