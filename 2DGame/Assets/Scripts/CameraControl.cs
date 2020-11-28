using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("目標")]
    public Transform target;
    [Header("追蹤速度")]
    public float speed = 1.5f;
    [Header("攝影機上下方的限制")]
    public Vector2 limit = new Vector2(0, 3.5f);

    private void Track()
    {
        Vector3 posA = transform.position;
        Vector3 posB = target.position;

        posB.y = Mathf.Clamp(posB.y, limit.x, limit.y);
        posB.z = -10;

        posA = Vector3.Lerp(posA, posB, speed * Time.deltaTime);

        transform.position = posA;
    }

    private void LateUpdate()
    {
        Track();
    }
}
