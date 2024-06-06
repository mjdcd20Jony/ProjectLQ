using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenetoEnding : MonoBehaviour
{

    [SerializeField] private string nextSceneName;
    
    public void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadScene();
        }
    }
}
