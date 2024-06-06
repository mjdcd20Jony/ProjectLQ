using UnityEngine;

public class PlayerTeleportTrigger : MonoBehaviour
{
    // �÷��̾ �̵��� ��ǥ ��ġ�� �����մϴ�.
    [Header("�÷��̾ TP�� ��ġ")]
    [SerializeField] private Vector3 teleportPosition;

    [Header("2�� ����Ʈ �߰� - �� ��������, ���� ��������")]
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
