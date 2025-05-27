using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  
    private Rigidbody _rigidbody;
    private Vector2 _playerInput;
    [SerializeField] private  PlayerStats playerStats;
    [SerializeField] private bool IsGround;
    [SerializeField] private Transform groundCheckTr;
    
    
    void Start()
    {
    
    }
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }



    void Update()
    {
        HandleLook();
    }

    void FixedUpdate()
    {
        HandleMove();
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
        }
        else
        {
            _playerInput = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rigidbody.AddForce(Vector3.up * playerStats.JumpForce, ForceMode.Impulse);
        }
    }
    
    [SerializeField] private Transform cameraContainer;
    [SerializeField] private Transform camTr;
    private Vector2 lookInput;
    private float xRotation = 0f;
    public float mouseSensitivity = 1f;


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

    private void HandleMove()
    {
        Vector3 camForwad = camTr.forward;
        Vector3 camRight = camTr.right;

        camForwad.y = 0f;
        camRight.y = 0f;
        
        camForwad.Normalize();
        camRight.Normalize();

        Vector3 move = camRight * _playerInput.x + camForwad * _playerInput.y;
        Vector3 velocity = new Vector3(move.x * playerStats.Speed, _rigidbody.velocity.y, move.z * playerStats.Speed);
        
        _rigidbody.velocity = velocity;
    }


}
