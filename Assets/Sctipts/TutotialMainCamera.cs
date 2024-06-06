using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    public Transform player;       // �÷��̾��� Transform
    public Vector2 minPosition;    // ī�޶��� �ּ� ��ġ
    public Vector2 maxPosition;    // ī�޶��� �ִ� ��ġ
    public float smoothTime = 0.3f; // ī�޶� �������� �ε巯��
    public float aheadDistance = 2.0f; // �÷��̾� �������� ������ �Ÿ�
    public float speedThreshold = 0.1f; // �÷��̾� �ӵ� �Ӱ谪

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetPosition = player.position;   

        // �÷��̾��� �ӵ��� �����Ͽ� ���� ����
        float playerSpeed = player.GetComponent<Rigidbody2D>().velocity.x;

        if (playerSpeed > speedThreshold)
        {
            targetPosition.x += aheadDistance;
        }
        else if (playerSpeed < -speedThreshold)
        {
            targetPosition.x -= aheadDistance;
        }

        targetPosition.z = transform.position.z; // ī�޶��� Z�� ��ġ ����

        // ī�޶��� ��ġ�� �ε巴�� ���󰡰� �ϱ�
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // ī�޶��� ��ġ�� ���� ��� ���� ����
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minPosition.x, maxPosition.x);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minPosition.y, maxPosition.y);

        // ī�޶��� ��ġ ������Ʈ
        transform.position = smoothedPosition;
    }
}

