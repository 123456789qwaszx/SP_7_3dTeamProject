using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
  
    private Rigidbody _rigidbody;
    private Vector2 _playerInput;
    Animator animator;
    
    
    [SerializeField] private  PlayerStats playerStats;
    
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
       
    }

    void FixedUpdate()
    {
        HandleMove();
    }

    public void CheckGround()
    {
        IsGround = Physics.CheckSphere(groundCheckTr.position, groundDistance, groundMask);
        if (!IsGround)
        {
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

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerInput = context.ReadValue<Vector2>();
            // Debug.Log($"Cheak{_playerInput}");
        }
        else
        {
            _playerInput = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // Debug.Log("Jump입력 됨");
        if (context.performed && IsGround)
        {
            // Debug.Log("true로 못 들어오는 중");
            
            animator.SetBool("IsJump", true);
            
        }
        // Debug.Log("현재 땅이 아닙니다.");
    }

    public void AddJump()
    {
        _rigidbody.AddForce(Vector3.up * playerStats.JumpForce, ForceMode.Impulse);
       
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
        if (context.performed)
        {
            // Debug.Log("마우스 좌클릭");
            animator.SetTrigger("IsAttack");
        }
        // Debug.Log("마우스 좌클릭이 안됨");
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


}
