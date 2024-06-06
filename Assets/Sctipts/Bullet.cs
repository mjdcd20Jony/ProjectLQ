using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField] private float collisionForce = 10f;
    [SerializeField] private float jumpForce = 10f;

    // �ǰ� ���带 ���� ���� �߰�
    public AudioClip hitSound;
    private AudioSource audioSource;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;

        // ����� �ҽ� �ʱ�ȭ
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Floor") || other.CompareTag("Platform"))
        {
            Destroy(gameObject); // �Ѿ� �ı�
        }

        else if (other.CompareTag("Player"))
        {
            // �ǰ� ���� ���
            audioSource.PlayOneShot(hitSound);

            Rigidbody2D playerRigidbody2D = other.GetComponent<Rigidbody2D>();

            if (playerRigidbody2D != null)
            {
                playerRigidbody2D.AddForce(new Vector2(collisionForce, jumpForce), ForceMode2D.Impulse);
            }

            Destroy(gameObject, hitSound.length); // �Ѿ� �ı��� ���� ���� �ķ� ����
        }
    }
}
