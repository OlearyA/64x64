using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using System.Collections;

public class Player : MonoBehaviour
{
    public bool Alive;
    public GameObject[] DeadPlayers;
    [SerializeField]
    private float groundedMoveSpeed,airMoveSpeed,groundedVelocityCap,airVelocityCap,intiJumpSpeed,jumpSpeed,coyoteTimer,jumpDuration;
    [SerializeField]
    private Vector2 spawnPoint;
    [SerializeField]//temp
    private bool _coyote,_jumping;
    private Vector2 _movement;
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private bool _grounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Alive)
        {
            _rigidbody.freezeRotation = true;
            _rigidbody.linearVelocity = Vector2.zero;
            Vector3 t = transform.position;
            //t.x/.4f
            Instantiate(DeadPlayers[0], transform.position, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        if (_grounded)
        {
            _rigidbody.AddForce(new Vector2(_movement.x * groundedMoveSpeed, 0));
            if (_rigidbody.linearVelocityX > groundedVelocityCap)
            {
                _rigidbody.linearVelocityX=groundedVelocityCap;
            }
            else if (_rigidbody.linearVelocityX < -groundedVelocityCap)
            {
                _rigidbody.linearVelocityX=-groundedVelocityCap;
            }
        }

        if (_jumping)
        {
            _rigidbody.AddForce(Vector2.up* jumpSpeed);
        }
        else _rigidbody.AddForce( new Vector2(_movement.x * airMoveSpeed,0),ForceMode2D.Impulse);
        if (_rigidbody.linearVelocityX > airVelocityCap)
        {
            _rigidbody.linearVelocityX=airVelocityCap;
        }
        else if (_rigidbody.linearVelocityX < -airVelocityCap)
        {
            _rigidbody.linearVelocityX=-airVelocityCap;
        }
    }
    
    public void UpdateMovement(InputAction.CallbackContext content)
    {
        _movement = content.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext content)
    {
        if (!content.ReadValueAsButton())
        {
            _jumping = false;
            return;
        }
        if(!_grounded) return;
        if(_jumping) return;
        StartCoroutine(JumpTimer(jumpDuration));
        _rigidbody.AddForce(Vector2.up* intiJumpSpeed,ForceMode2D.Impulse);
        _jumping = true;
        _coyote = false;
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
            _grounded=true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;
        _coyote=true;
        _grounded=false;
        StartCoroutine(CoyoteTimer(coyoteTimer));
    }
    private IEnumerator CoyoteTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        _coyote=false;
    }
    private IEnumerator JumpTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        _jumping = false;
    }
    
}