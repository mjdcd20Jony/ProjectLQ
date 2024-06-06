using UnityEngine;
using System.Collections;

public class FuelManager : MonoBehaviour
{
    #region ���� ���� ����
    [Header("�÷��̾�� ����")]
    public Transform playerTransform;

    [Header("������ų �� Max �� 10 - ���̵��� �°� ����")]
    public float refuelAmount = 10f;

    [Header("��Ÿ��")]
    public float coolTime = 15f;
    private bool isCooling = false;
    private float elapsedTime = 0f;

    private Animator restoreAreaAnimation;

    private TorchController controller;
    #endregion

    #region �⺻ �޼��� : Awake, Update
    void Awake()
    {
        controller = GameObject.Find("PlayerLight")?.GetComponent<TorchController>();
        if (controller == null)
        {
            Debug.LogError("PlayerLight ������Ʈ�� ���ų� TorchController ������Ʈ�� ã�� �� �����ϴ�.");
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

    #region ������ ���� �ڵ�
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

    #region �浹 ���� �ڵ� �߰�
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
