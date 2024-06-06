using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform firePoint; // 총알이 발사될 위치

    // 총알 발사 함수
    public void SpawnBullet()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
