using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotoStageCutScene : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private Vector3 targetScale;
    [SerializeField] private float speed;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AudioClip scalingSound; // 추가된 부분
    private AudioSource audioSource; // 추가된 부분

    private Vector3 fixedPlayerPosition = new Vector3(302.6f, -29.3f, 0);
    private bool isScalingComplete = false;

    void Start()
    {
        platform.SetActive(false);

        // AudioSource 초기화
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = scalingSound;

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
            Debug.LogError("PlayerController가 할당되지 않았습니다.");
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

        audioSource.Play();

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

        audioSource.Stop();
    }
}
