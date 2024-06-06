using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerMove : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾��� ��� ��ũ��Ʈ�� ��Ȱ��ȭ
            MonoBehaviour[] scripts = other.gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }

            // Rigidbody2D�� �����ͼ� �ӵ��� ����
            Rigidbody2D playerRigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody2D != null)
            {
                playerRigidbody2D.velocity = new Vector2(5, 0); // X �������� �̵�

                // �߷� ������ ���� �ʰ� ���� (���� ����)
                playerRigidbody2D.gravityScale = 0;

                // ����� �����Ͽ� �ﰢ������ �̵��ϰ� ���� (�ʿ�� �߰�)
                playerRigidbody2D.AddForce(new Vector2(5, 0), ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MonoBehaviour[] scripts = other.gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = true;
            }
        }
    }
}
