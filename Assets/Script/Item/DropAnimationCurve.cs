using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAnimationCurve : MonoBehaviour
{
    public AnimationCurve Curve;
    public float duration = 0.5f;
    public float maxHeight = 0.5f;

    public float rotateSpeed = 10f;
    private float timer = 0f;

    private float lastOffsetY = 0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float normalizedTime = (timer % duration) / duration;
        float curveValue = Curve.Evaluate(normalizedTime);

        float offsetY = curveValue * maxHeight;

        transform.position = transform.position - new Vector3(0, lastOffsetY, 0) + new Vector3(0, offsetY, 0);

        lastOffsetY = offsetY;

        transform.rotation *= Quaternion.Euler(0, rotateSpeed * Time.deltaTime, 0);
    }
}
