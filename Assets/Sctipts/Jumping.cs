using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    [SerializeField] private Animator mushroom;
    
    private void Awake()
    {
        
    }
    private void Start()
    {
        mushroom = GetComponent<Animator>();
        mushroom.SetBool("isPlayer", false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� �浹");
            mushroom.SetBool("isPlayer", true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ����");
            mushroom.SetBool("isPlayer", false);
        }
    }
}
