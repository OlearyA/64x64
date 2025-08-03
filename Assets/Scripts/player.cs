using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed,airMoveSpeed,jumpSpeed,coyoteTimer;
    
    private Vector2 _movement;
    private Rigidbody2D _rigidbody;
    
    private bool _grounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
          if(_grounded)  
              _rigidbody.AddForce( new Vector2( _movement.x * moveSpeed,_rigidbody.linearVelocity.y));
           else _rigidbody.AddForce( new Vector2((_movement.x * airMoveSpeed)+_rigidbody.linearVelocity.x,_rigidbody.linearVelocity.y));
    }
    
    public void UpdateMovement(InputAction.CallbackContext content)
    {
        _movement = content.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext content)
    {
        if (!_grounded)
        {
            _rigidbody.AddForce(Vector2.up *(.25f*jumpSpeed));
            return;
        }
        _rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        _grounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
            _grounded=true;
    }
    
}