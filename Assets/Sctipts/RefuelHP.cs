using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelHP : MonoBehaviour
{
    [Header("증가시킬 양 Max 값 10 - 난이도에 맞게 수정")]
    private float refuelAmount = 10f;

    private Animator restoreAreaAnimation;

    private TorchController controller;

    [Header("쿨타임")]
    public float coolTime = 15f;
    private bool isCooling = false;
    private float elapsedTime = 0f;

    [Header("SFX")]
    private AudioSource audioSource;
    [SerializeField]private AudioClip resourceEffectSound;


    private void Awake()
    {
        controller = GameObject.Find("PlayerLight")?.GetComponent<TorchController>();

        if (controller == null)
        {
            Debug.LogError("PlayerLight 오브젝트가 없거나 TorchController 컴포넌트를 찾을 수 없습니다.");
        }
    }

    void Start()
    {
        restoreAreaAnimation = GetComponent<Animator>();
        restoreAreaAnimation.SetBool("isRefuel", false);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = resourceEffectSound;
    }

    void Update()
    {
        if (isCooling)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= coolTime)
            {
                elapsedTime = 0f;
                isCooling = false;
            }
        }
    }

    public void Refuel()
    {
        if (controller != null)
        {
            restoreAreaAnimation.SetBool("isRefuel", true);
            controller.cur_Hp += refuelAmount;
            if (controller.cur_Hp > controller.max_Hp)
            {
                controller.cur_Hp = controller.max_Hp;
            }
            controller.timeElapsed = 0;
            controller.torchHPbar.value = controller.cur_Hp / controller.max_Hp;

            audioSource.Play();
            isCooling = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isCooling)
        {
            Refuel();
        }
    }

    private void OnTriggerExit2D(Collider2D other)  
    {
        if (other.gameObject.CompareTag("Player"))
        {
            restoreAreaAnimation.SetBool("isRefuel", false);
        }
    }

}