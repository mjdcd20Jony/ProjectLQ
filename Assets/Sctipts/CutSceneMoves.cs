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

    void Start()
    {
        moveSpeed = 3f;
        StartCoroutine(MoveToTarget());
        lastscenes.SetActive(false);
    }

    void Update()
    {
        if (transform.position == targetPosition)
        {
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
        yield return new WaitForSeconds(2f);
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
