using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUI : MonoBehaviour
{
    public float moveSpeed = 2.0f; // ������Ʈ �̵� �ӵ�

    void Update()
    {
        // ������Ʈ�� ���� ��� �̵�
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
