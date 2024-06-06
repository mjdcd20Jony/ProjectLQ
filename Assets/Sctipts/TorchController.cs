using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;


public class TorchController : MonoBehaviour
{
    #region 변수 선언 영역
    [Header("빛 범위 조절 변수")]
    public float minRange = 0f; // 최소 빛 범위
    public float maxRange = 15f; // 최대 빛 범위
    [Header("빛이 꺼질 때 까지 걸리는 시간")]
    public float light_off_duration = 60f; // 빛 범위가 변경되는데 걸리는 시간 (초)

    public float timeElapsed = 0f;

    [Header("아래에 플레이어 넣기")]
    public Transform player_Position;

    private Light2D light2D;

    [Header("Light HP")]
    [SerializeField] public Slider torchHPbar;
    [SerializeField] public float max_Hp =100f; 
    [SerializeField] public float cur_Hp;

    private float currentRange; // 현재 빛 범위
    #endregion

    #region 메서드 선언 영역
    public void Start()
    {
        light2D = GetComponent<Light2D>();
        maxRange = 100f;
        max_Hp = maxRange;
        cur_Hp = max_Hp; 
        torchHPbar.value = cur_Hp / max_Hp;
        currentRange = maxRange;

        light_off_duration = 60f;
    }

    public void Update()
    {
        timeElapsed += Time.deltaTime;

        float t = Mathf.Clamp01(timeElapsed / light_off_duration);
        float lightrange = Mathf.Lerp(maxRange, minRange, t);

        if (lightrange != currentRange)
        {
            cur_Hp = lightrange;
            torchHPbar.value = cur_Hp / max_Hp;
            currentRange = lightrange;
        }

        light2D.pointLightOuterRadius = lightrange;

    }

    void LateUpdate()
    {
        if (player_Position != null)
        {
            transform.position = new Vector3(player_Position.position.x, player_Position.position.y, transform.position.z);
        }
    }
    #endregion
}
