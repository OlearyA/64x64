using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AO.Scripts
{
    public class Player : MonoBehaviour
    {
        public GameObject[] DeadPlayers;
        [SerializeField]
        private float groundedMoveSpeed,airMoveSpeed,groundedVelocityCap,airVelocityCap,intiJumpSpeed,jumpSpeed,coyoteTimer,jumpDuration,respawnTimer,castDistance;
        [SerializeField]
        private Vector2 spawnPoint,boxCastSize;
        [SerializeField]
        LayerMask groundLayer;
        [SerializeField]//temp
        private bool _coyote,_jumping,_alive=true;
        private Vector2 _movement;
        private Rigidbody2D _rigidbody;
        private BoxCollider2D _boxCollider2d;
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private bool _grounded=> IsGrounded();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rigidbody=GetComponent<Rigidbody2D>();
            _boxCollider2d = GetComponent<BoxCollider2D>();
            spawnPoint=transform.position;
            _spriteRenderer=GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void FixedUpdate()
        {
            if(!_alive)return;
            if (IsGrounded())
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
            if(!IsGrounded()) return;
            if(_jumping) return;
            StartCoroutine(JumpTimer(jumpDuration));
            _rigidbody.AddForce(Vector2.up* intiJumpSpeed,ForceMode2D.Impulse);
            _jumping = true;
            _coyote = false;
        
        }

        public bool IsGrounded()
        {
            if (Physics2D.BoxCast(transform.position, boxCastSize, 0, -transform.up, castDistance,groundLayer))
            {
                return true;
            }
            else return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position-transform.up*castDistance,boxCastSize);
        }
        
/*
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
        */
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

        public void Death()
        {
            // animation maybe
            if(!_alive) return;
            _alive=false;
            _rigidbody.freezeRotation = true;
            _rigidbody.linearVelocity = Vector2.zero;
            Vector3 p  = transform.position;
            Instantiate(DeadPlayers[0], p, Quaternion.identity);
            _spriteRenderer.enabled = false;
            transform.position = spawnPoint;
            transform.rotation = Quaternion.identity;
            Invoke("Respawn", respawnTimer);
        }

        private void Respawn()
        {
            _spriteRenderer.enabled = true;
            CameraManager.Instance.RemoveCamera();
            _alive=true;
        }
    
    }
}