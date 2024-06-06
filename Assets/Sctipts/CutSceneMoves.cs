using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneMoves : MonoBehaviour
{
    public Vector3 originalPosition;
    public Vector3 targetPosition;
    public float moveSpeed;
    public GameObject lastscenes;
    public string nextSceneName;
    public AudioSource soundEffect;
    public AudioClip soundClip;  // ����� Ŭ�� �ʵ� �߰�
    private bool isMoving = true; // �̵� ������ ���θ� ��Ÿ���� �÷���

    void Start()
    {
        moveSpeed = 3f;
        StartCoroutine(MoveToTarget());
        lastscenes.SetActive(false);

        // ����� Ŭ���� AudioSource�� �Ҵ�
        if (soundEffect != null && soundClip != null)
        {
            soundEffect.clip = soundClip;
        }
    }

    void Update()
    {
        if (isMoving && transform.position == targetPosition)
        {
            isMoving = false; // �̵��� �������� ǥ��
            StartCoroutine(ActivateLastSceneAndWait());
        }
    }

    IEnumerator MoveToTarget()
    {
        float elapsedTime = 0;
        float journeyLength = Vector3.Distance(originalPosition, targetPosition);

        while (elapsedTime < journeyLength / moveSpeed)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime * moveSpeed / journeyLength);
            yield return null;
        }

        transform.position = targetPosition;
    }

    IEnumerator ActivateLastSceneAndWait()
    {
        lastscenes.SetActive(true);
        if (soundEffect != null)
        {
            soundEffect.Play();
        }
        yield return new WaitForSeconds(2f);
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
