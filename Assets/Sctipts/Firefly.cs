using UnityEngine;

public class FireFly : MonoBehaviour
{
    [Header("공전 속도 및 거리 조절")]
    [SerializeField] private float speed =1f;
    [SerializeField] private float radius = 0.04f;
    [SerializeField] private Transform player;

    /*[Header("공전 궤도면 각도 조절")]
    [SerializeField] private float orbital_Angular = 45f; // 공전 궤도 각도
    [SerializeField] private float orbital_Angular_IncreaseRate = 5f; //각도 상승량*/

    private float moving_angle = 0.0f;

    void Start()
    {
        radius = 0.04f;
        //orbital_Angular_IncreaseRate = 5f;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        moving_angle += speed * Time.deltaTime;
        
        //orbital_Angular += orbital_Angular_IncreaseRate * Time.deltaTime;

        //float x = Mathf.Cos(moving_angle + orbital_Angular) * radius;
        float x = Mathf.Cos(moving_angle) * radius;
        float y = Mathf.Sin(moving_angle) * radius;


        
        transform.position = player.position + new Vector3(x, y, 0);

      
    }
}
