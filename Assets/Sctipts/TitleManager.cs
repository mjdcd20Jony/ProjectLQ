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

    // 다음 씬을 로드하는 메서드
    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
