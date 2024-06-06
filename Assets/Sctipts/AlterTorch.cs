using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterTorch : MonoBehaviour
{
    [SerializeField] private bool isPlayerGet;
    void Start()
    {
        isPlayerGet = false;       
    }

    void Update()
    {
        if (isPlayerGet == true)
        { 
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerGet = true;
        }
    }
}
