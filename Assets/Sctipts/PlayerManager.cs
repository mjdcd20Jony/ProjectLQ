using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerTransform;

    [Header("플레이어 라이트")]
    [SerializeField] private TorchController playerlight;

    [SerializeField] private Vector3 teleportPosition;

    void Update()
    {
        if (playerlight.cur_Hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        player.transform.position = teleportPosition;
        if (playerlight != null)
        {
            playerlight.cur_Hp += playerlight.max_Hp;
            if (playerlight.cur_Hp > playerlight.max_Hp)
            {
                playerlight.cur_Hp = playerlight.max_Hp;
            }
            playerlight.timeElapsed = 0;
            playerlight.torchHPbar.value = playerlight.cur_Hp / playerlight.max_Hp;
        }
    }
}
