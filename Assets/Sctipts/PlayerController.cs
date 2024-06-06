using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region [변수 선언 영역]
    private Rigidbody2D playerRigidbody2D;
    private Transform playerTransform;

    [Header("====>점프 처리 관련 변수<====")]
    [SerializeField] private float jumpforceIncreaseRate = 200f;
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private short jumpRemain = 1;
    [SerializeField] private float currentJumpForce = 0f;
    const float jumpMinForce = 50f;
    const float jumpMaxForce = 800f;

    [Header("점프처리관련 - 확인용 - 절대 수정 금지")]
    [SerializeField] private bool isPlatform = true;

    [Header("====>이동 처리 관련 변수<====")]
    [SerializeField] private float moveSpeedDefault = 6f;
    [SerializeField] private float moveSpeed = 0f;
    private float whenChargeSpeed = 0f;

    [Header("====>이 곳에 플레이어 애니메이션<====")]
    [SerializeField] private Animator playerAnimator;

    [Header("====>Torch 오브젝트 & UI 오브젝트<====")]
    public GameObject torchObject;
    public GameObject hp_BAR;

    [Header("# 플레이어 사운드 이펙트")]
    private AudioSource playerAudioSource;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip runningClip;

    private bool isRunning = false;

    #endregion

    #region [기본 메서드 선언 영역] - Awake, Start, Update
    private void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        if (playerRigidbody2D == null)
        {
            
        }
        if (playerAnimator == null)
        {
            
        }
    }

    private void Start()
    {
        moveSpeedDefault = 6f;
        moveSpeed = moveSpeedDefault;

        if (hp_BAR != null)
        {
            hp_BAR.SetActive(false);
        }
        else
        {
            
        }

        if (torchObject != null)
        {
            torchObject.SetActive(false);
        }
        else
        {
            
        }

        jumpForce = 0f;
        jumpRemain = 1;

        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isTorch", false);
        }

        playerAudioSource = GetComponent<AudioSource>();
        if (playerAudioSource == null)
        {
            playerAudioSource = gameObject.AddComponent<AudioSource>();
        }

        if (jumpClip == null)
        {
            
        }

        if (runningClip == null)
        {
            
        }
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
    }
    #endregion

    #region [이동 처리 관련 메서드]
    private void HandleMovementInput()
    {
        if (isPlatform)
        {
            float xInput = Input.GetAxis("Horizontal");
            float xSpeed = moveSpeed * xInput;
            playerRigidbody2D.velocity = new Vector2(xSpeed, playerRigidbody2D.velocity.y);
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("isRunning", true);
            }

            if (!isRunning)
            {
                playerAudioSource.clip = runningClip;
                playerAudioSource.loop = true;
                playerAudioSource.Play();
                isRunning = true;
            }

            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("isRunning", false);
            }

            if (isRunning)
            {
                playerAudioSource.Stop();
                isRunning = false;
            }
        }
    }
    #endregion

    #region [점프 관련 메서드 선언 영역]
    public void HandleJumpInput()
    {
        if (Input.GetKey(KeyCode.Space) && isPlatform && jumpRemain > 0)
        {
            if (currentJumpForce <= jumpMaxForce - jumpMinForce)
            {
                currentJumpForce += jumpforceIncreaseRate * Time.deltaTime;
                if (playerAnimator != null)
                {
                    playerAnimator.SetBool("isChargingStart", true);
                }
            }

            moveSpeed = whenChargeSpeed;
        }
        if (Input.GetKeyUp(KeyCode.Space) && isPlatform && jumpRemain > 0)
        {
            jumpForce = currentJumpForce + jumpMinForce;
            currentJumpForce = 0f;

            playerRigidbody2D.AddForce(new Vector2(moveSpeedDefault, jumpForce));

            if (playerAnimator != null)
            {
                playerAnimator.SetBool("isChargingEnd", true);
                playerAnimator.SetBool("isChargingStart", false);
            }

            moveSpeed = moveSpeedDefault;

            if (jumpClip != null)
            {
                playerAudioSource.PlayOneShot(jumpClip);
            }
            else
            {
                
            }

            jumpForce = 0f;
            jumpRemain -= 1;
        }
    }
    #endregion

    #region [충돌 처리 관련 메서드]
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("MovingPlatform"))
        {
            isPlatform = true;
            jumpRemain = 1;

            if (playerAnimator != null)
            {
                playerAnimator.SetBool("isJump", false);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("MovingPlatform"))
        {
            isPlatform = true;

            if (playerAnimator != null)
            {
                playerAnimator.SetBool("isJump", false);
            }

            jumpRemain = 1;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("MovingPlatform"))
        {
            isPlatform = false;
            jumpRemain = 0;

            if (playerAnimator != null)
            {
                playerAnimator.SetBool("isJump", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Torch"))
        {
            if (torchObject != null)
            {
                torchObject.SetActive(true);
                if (hp_BAR != null)
                {
                    hp_BAR.SetActive(true);
                }

                if (playerAnimator != null)
                {
                    playerAnimator.SetBool("isTorch", true);
                }
            }
            other.gameObject.SetActive(false);
        }
    }
    #endregion
}
