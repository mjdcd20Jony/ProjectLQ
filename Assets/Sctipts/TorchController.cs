using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;


public class TorchController : MonoBehaviour
{
    #region ���� ���� ����
    [Header("�� ���� ���� ����")]
    public float minRange = 0f; // �ּ� �� ����
    public float maxRange = 15f; // �ִ� �� ����
    [Header("���� ���� �� ���� �ɸ��� �ð�")]
    public float light_off_duration = 60f; // �� ������ ����Ǵµ� �ɸ��� �ð� (��)

    public float timeElapsed = 0f;

    [Header("�Ʒ��� �÷��̾� �ֱ�")]
    public Transform player_Position;

    private Light2D light2D;

    [Header("Light HP")]
    [SerializeField] public Slider torchHPbar;
    [SerializeField] public float max_Hp =100f; 
    [SerializeField] public float cur_Hp;

    private float currentRange; // ���� �� ����
    #endregion

    #region �޼��� ���� ����
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
