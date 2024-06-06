using UnityEngine;

public class PlayerTeleportTrigger : MonoBehaviour
{
    // 플레이어가 이동할 목표 위치를 설정합니다.
    [Header("플레이어가 TP할 위치")]
    [SerializeField] private Vector3 teleportPosition;

    [Header("2개 리스트 추가 - 현 스테이지, 다음 스테이지")]
    [SerializeField] private GameObject[] stages;

    private void Start()
    {
        stages[0].SetActive(true);
        stages[1].SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TeleportPlayer(other.gameObject);

            stages[0].SetActive(false);
            stages[1].SetActive(true);

        }
    }

    private void TeleportPlayer(GameObject player)
    {
        player.transform.position = teleportPosition;
    }
}
