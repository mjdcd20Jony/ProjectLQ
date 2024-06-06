using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUI : MonoBehaviour
{
    public float moveSpeed = 2.0f; // 오브젝트 이동 속도

    void Update()
    {
        // 오브젝트를 위로 계속 이동
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
