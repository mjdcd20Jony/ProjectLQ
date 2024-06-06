using UnityEngine;
using System.Collections;

public class FuelManager : MonoBehaviour
{
    #region 변수 선언 영역
    [Header("플레이어와 연결")]
    public Transform playerTransform;

    [Header("증가시킬 양 Max 값 10 - 난이도에 맞게 수정")]
    public float refuelAmount = 10f;

    [Header("쿨타임")]
    public float coolTime = 15f;
    private bool isCooling = false;
    private float elapsedTime = 0f;

    private Animator restoreAreaAnimation;

    private TorchController controller;
    #endregion

    #region 기본 메서드 : Awake, Update
    void Awake()
    {
        controller = GameObject.Find("PlayerLight")?.GetComponent<TorchController>();
        if (controller == null)
        {
            Debug.LogError("PlayerLight 오브젝트가 없거나 TorchController 컴포넌트를 찾을 수 없습니다.");
        }
    }

    private void Start()
    {
        restoreAreaAnimation = GetComponent<Animator>();
        restoreAreaAnimation.SetBool("isRefuel", false);
    }

    void Update()
    {
        if (isCooling)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= coolTime)
            {
                isCooling = false;
                elapsedTime = 0f;
            }
        }
    }
    #endregion

    #region 재충전 관련 코드
    public void StartRefuelProcess()
    {
        if (!isCooling)
        {
            Refuel();
        }
    }

    private void Refuel()
    {
        if (controller != null)
        {
            restoreAreaAnimation.SetBool("isRefuel",true);
            controller.cur_Hp += refuelAmount; 
            if (controller.cur_Hp > controller.max_Hp)
            {
                controller.cur_Hp = controller.max_Hp;
            }
            controller.timeElapsed = 0;
            controller.torchHPbar.value = controller.cur_Hp / controller.max_Hp;

            isCooling = true;
        }
    }

    private IEnumerator ShowPressEMask()
    {
        yield return new WaitForSeconds(0.3f);
    }
    #endregion

    #region 충돌 감지 코드 추가
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartRefuelProcess();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            restoreAreaAnimation.SetBool("isRefuel", false);
        }
    }
    #endregion
}
