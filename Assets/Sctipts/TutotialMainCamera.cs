using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    public Transform player;       // 플레이어의 Transform
    public Vector2 minPosition;    // 카메라의 최소 위치
    public Vector2 maxPosition;    // 카메라의 최대 위치
    public float smoothTime = 0.3f; // 카메라 움직임의 부드러움
    public float aheadDistance = 2.0f; // 플레이어 앞쪽으로 보여줄 거리
    public float speedThreshold = 0.1f; // 플레이어 속도 임계값

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetPosition = player.position;   

        // 플레이어의 속도를 감지하여 방향 설정
        float playerSpeed = player.GetComponent<Rigidbody2D>().velocity.x;

        if (playerSpeed > speedThreshold)
        {
            targetPosition.x += aheadDistance;
        }
        else if (playerSpeed < -speedThreshold)
        {
            targetPosition.x -= aheadDistance;
        }

        targetPosition.z = transform.position.z; // 카메라의 Z축 위치 고정

        // 카메라의 위치를 부드럽게 따라가게 하기
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // 카메라의 위치를 맵의 경계 내로 제한
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minPosition.x, maxPosition.x);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minPosition.y, maxPosition.y);

        // 카메라의 위치 업데이트
        transform.position = smoothedPosition;
    }
}

