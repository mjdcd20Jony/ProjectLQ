using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class TitleManager : MonoBehaviour
{
    public string nextSceneName;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadNextScene();
        }
    }

    // ���� ���� �ε��ϴ� �޼���
    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
