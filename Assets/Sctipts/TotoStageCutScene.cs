using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotoStageCutScene : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private Vector3 targetScale;
    [SerializeField] private float speed;
    [SerializeField] private PlayerController playerController; // �巡�� �� ������� �����Ϳ��� �Ҵ��� �� �ְ� ����

    private Vector3 fixedPlayerPosition = new Vector3(302.6f, -29.3f, 0);
    private bool isScalingComplete = false;

    void Start()
    {
        platform.SetActive(false);

        // playerController�� �������� �Ҵ����� �ʴ´ٸ�, ã�Ƽ� �Ҵ�
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        if (playerController != null)
        {
            StartCoroutine(ScaleOverTime(targetScale, speed));
        }
        else
        {
            Debug.LogError("PlayerController�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    private void Update()
    {
        if (!isScalingComplete && playerController != null)
        {
            playerController.transform.position = fixedPlayerPosition;
        }
    }

    IEnumerator ScaleOverTime(Vector3 targetScale, float duration)
    {
        Vector3 originalScale = transform.localScale;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        isScalingComplete = true;
        platform.SetActive(true);
        gameObject.SetActive(false);
    }
}
