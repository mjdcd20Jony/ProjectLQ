using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField] private float collisionForce = 10f;
    [SerializeField] private float jumpForce = 10f;

    // 피격 사운드를 위한 변수 추가
    public AudioClip hitSound;
    private AudioSource audioSource;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;

        // 오디오 소스 초기화
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Floor") || other.CompareTag("Platform"))
        {
            Destroy(gameObject); // 총알 파괴
        }

        else if (other.CompareTag("Player"))
        {
            // 피격 사운드 재생
            audioSource.PlayOneShot(hitSound);

            Rigidbody2D playerRigidbody2D = other.GetComponent<Rigidbody2D>();

            if (playerRigidbody2D != null)
            {
                playerRigidbody2D.AddForce(new Vector2(collisionForce, jumpForce), ForceMode2D.Impulse);
            }

            Destroy(gameObject, hitSound.length); // 총알 파괴를 사운드 길이 후로 지연
        }
    }
}
