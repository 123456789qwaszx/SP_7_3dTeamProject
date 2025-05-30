using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour, IDamageable
{
  
    private Rigidbody _rigidbody;
    private Vector2 _playerInput;
    Animator animator;
    
    
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Collider attackRange;
    [SerializeField] private Transform modelTransform;

    private List<IDamageable> targetsInRange = new();
    
    [Header("땅(그라운드) 체크")]
    [SerializeField] private bool IsGround;
    [SerializeField] private Transform groundCheckTr;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundMask;

    [Header("카메라")]
    [SerializeField] private Transform cameraContainer;
    [SerializeField] private Transform camTr;
    private Vector2 lookInput;
    private float xRotation = 0f;
    public float mouseSensitivity = 1f;

    [Header("SFX")]
    public SFXManager sfxManager;

    [Header("UIOpenClose")]
    [SerializeField] private UIOpenClose uiOpenClose;

    private bool isDead = false;//다이상태인지 체크
    private bool isJumping = false;//점프상태인지 체크


    void Start()
    {
    
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }



    void Update()
    {
        CheckGround();
        HandleLook();
        
        //Hunger.Subtract(noHungerHealthDecay * Time.deltaTime);
       playerStats.Hunger.Subtract(playerStats.Hunger.passiveValue * Time.deltaTime);
        playerStats.Hydration.Subtract(playerStats.Hydration.passiveValue * Time.deltaTime);
        
        if (playerStats.Hydration.curValue <= 0f)
        {
            playerStats.Health.Subtract(playerStats.noHungerHealthDecay * Time.deltaTime);
        }
        if (playerStats.Hunger.curValue <= 0f)
        {
            playerStats.Health.Subtract(playerStats.noHungerHealthDecay * Time.deltaTime);
        }
        // if (playerStats.Health.curValue <= 0f && !isDead)
        // {
        //     isDead = true;
        //     sfxManager.PlaySFX(sfxManager.playerDieSFX, transform.position);
        //     Debug.Log("Update 데미지 게임오버");
        //     GameManager.Instance.GameOver();
        // }
        
    }

    void FixedUpdate()
    {
        HandleMove();
        RotateModel();
    }

    public void CheckGround()
    {
        IsGround = Physics.CheckSphere(groundCheckTr.position, groundDistance, groundMask);
        if (!IsGround)
        {
            isJumping = false;
            animator.SetBool("IsJump", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckTr != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(groundCheckTr.position, groundDistance);
        }
    }
    public void Move()
    {
        
        Vector3 move = new Vector3(_playerInput.x, 0, _playerInput.y);
        _rigidbody.MovePosition(this.transform.position + move *playerStats.Speed  * Time.deltaTime);

    }

    public void OnMove(InputAction.CallbackContext context)  // 플레이어 이동 기능 (인풋시스템)
    {
        if (context.performed)
        {
            _playerInput = context.ReadValue<Vector2>();     // 입력 받은 값을 _playerInput 넣어줌
        }
        else
        {
            _playerInput = Vector2.zero;                     // 입력 받은 값이 없으면 Vector2.zeor를 _playerInput 넣어줌
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump입력 됨");
        //if (context.performed && IsGround)                   // 입력 받은 값이 맞고 IsGround가 True면 "점프 애니메이션" 실행
        if (isJumping || !IsGround || !context.performed) return;
        {
            Debug.Log("true로 못 들어오는 중");

            animator.SetBool("IsJump", true);          // 점프 애니메이션을 true로 바꿔줌
            //sfxManager.PlaySFX(sfxManager.playerJumpSFX, transform.position);
            //isJumping = true;
        }
        Debug.Log("현재 땅이 아닙니다.");
    }

    public void ApplyJump()                                // 애니메이션에서 클립 위치에 이벤트 줄 메서드
    {
        _rigidbody.AddForce(Vector3.up * playerStats.JumpForce, ForceMode.Impulse); // 점프 기능
    }
    
 

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void HandleLook()
    {
        Vector2 mouseDelta = lookInput * mouseSensitivity;
        
        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -65f, 65f);
        
        
        cameraContainer.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        transform.Rotate(Vector3.up * mouseDelta.x);
    }

    public void OnAttacking(InputAction.CallbackContext context)
    {
        //if (context.performed)                              // 마우스 클릭이 실행 됐을 때
        //{
        //    Debug.Log("마우스 좌클릭");
        //    animator.SetTrigger("IsAttack");           // 클릭이 실행 되면 "어택 애니메이션" 실행
        //    sfxManager.PlaySFX(sfxManager.playerAttackSFX, transform.position);
        //}
        //Debug.Log("마우스 좌클릭이 안됨");
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            // UI 위 클릭이면 무시
            Debug.Log("UI 클릭이므로 공격 안 함");
            return;
        }
        if ((uiOpenClose.option != null && uiOpenClose.option.activeSelf) ||
        (uiOpenClose.inven != null && uiOpenClose.inven.activeSelf))
        {
            Debug.Log("UI 창이 열려 있어서 공격 소리 차단");
            return;
        }
        if (context.performed)
        {
            Debug.Log("마우스 좌클릭 (공격)");
            animator.SetTrigger("IsAttack");
            sfxManager.PlaySFX(sfxManager.playerAttackSFX, transform.position);
        }
    }

    public void ApplyDamage()
    {
        foreach (var target in targetsInRange)
        {
            if (target != null)
            {
                target.TakeDamage(playerStats.Attack);
                Debug.Log("공격시도");
            }
        }
    }

    private void HandleMove()
    {
        Vector3 camForwad = camTr.forward;
        Vector3 camRight = camTr.right;
        
        camForwad.y = 0f;
        camRight.y = 0f;
        
        camForwad.Normalize();
        camRight.Normalize();

        Vector3 move = camRight * _playerInput.x + camForwad * _playerInput.y;
        
        bool isMoving = move.sqrMagnitude > 0.01f;
        animator.SetBool("IsRun", isMoving);
        
        Vector3 velocity = new Vector3(move.x * playerStats.Speed, _rigidbody.velocity.y, move.z * playerStats.Speed);

        _rigidbody.velocity = velocity;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Resource"))      // 들어온 오브젝트의 태그 "Enemy" 또는 "Resource" 체크
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("적이 들어왔다");
            }
            else if (other.CompareTag("Resource"))
            {
                Debug.Log("자원이다");
            }
            
            if (other.TryGetComponent(out IDamageable damageable))          // 들어온 오브젝트(other)에 IDamageable이 있는지 확인, 있으면 damageble변수 에 넣어줌.
            {
                Debug.Log($"IDamageable 인터페이스 있음! => 대상: {other.gameObject.name}");
                if (!targetsInRange.Contains(damageable))
                    targetsInRange.Add(damageable);
                Debug.Log("리스트에 추가 완료");
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            targetsInRange.Remove(damageable);
        }
    }
    
    public void Heal(float amount)
    {
        playerStats.Health.Add(amount);
    }
    public void Drink(float amount)
    {
        playerStats.Hydration.Add(amount);
    }
    public void Eat(float amount)
    {
        playerStats.Health.Add(amount);
        playerStats.Hunger.Add(amount);
    }

    public void TakeDamage(float damage)
    {
        playerStats.Health.Subtract(damage);
        sfxManager.PlaySFX(sfxManager.playerHitSFX, transform.position);
        if (playerStats.Health.curValue <= 0f && !isDead)
        {
            isDead = true;
            Debug.Log("TakeDamage 게임오버");
            GameManager.Instance.GameOver();
        }
    }

    private void RotateModel()
    {
        Vector3 moveDir = _rigidbody.velocity;
        moveDir.y = 0;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            modelTransform.rotation = Quaternion.Slerp(
                modelTransform.rotation,
                targetRot,
                Time.deltaTime * 10f
            );
        }
    }
    public void PlayJumpSFX()
    {
        sfxManager.PlaySFX(sfxManager.playerJumpSFX, transform.position);
    }
}
