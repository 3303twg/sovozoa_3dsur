using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAnimationCurve : MonoBehaviour
{
    public AnimationCurve speedCurve;  // 에디터에서 곡선 직접 편집 가능
    public float duration = 3f;         // 움직임 지속 시간
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
            float normalizedTime = timer / duration;      // 0 ~ 1 사이
            float curveValue = speedCurve.Evaluate(normalizedTime);  // 곡선에 따라 속도값 얻기

            // 선형 이동에 곡선값 곱해서 자연스러운 이동
            transform.position = Vector3.Lerp(startPos, endPos, curveValue);
        }
        else
        {
            timer = 0f;
        }

        transform.rotation *= Quaternion.Euler(0, rotateSpeed * Time.deltaTime, 0);
    }
}
