using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerMove : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어의 모든 스크립트를 비활성화
            MonoBehaviour[] scripts = other.gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }

            // Rigidbody2D를 가져와서 속도를 설정
            Rigidbody2D playerRigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody2D != null)
            {
                playerRigidbody2D.velocity = new Vector2(5, 0); // X 방향으로 이동

                // 중력 영향을 받지 않게 설정 (선택 사항)
                playerRigidbody2D.gravityScale = 0;

                // 운동량을 설정하여 즉각적으로 이동하게 설정 (필요시 추가)
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
