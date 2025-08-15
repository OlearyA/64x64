using System;
using System.Collections;
using UnityEngine;

namespace AO.Scripts
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField]
        GameObject cannonBall;
        [SerializeField]
        float cannonBallSpeed,reloadTime,detectionOffset,cannonBallOffset;
        [SerializeField]
        Vector2 detectionBox;
        
        private SpriteRenderer _spriteRenderer;
        private bool _isReloading,_leftFaceing,_playerDetected;
        [SerializeField]
        LayerMask playerMask;

        private void Start()
        {
            _spriteRenderer=GetComponent<SpriteRenderer>();
            _leftFaceing=!_spriteRenderer.flipX;
        }

        private void Update()
        {
            if(_leftFaceing)
                if (Physics2D.BoxCast(transform.position, detectionBox, 0,Vector2.left, detectionOffset,playerMask))
                {
                    _playerDetected=true;
                }
                else _playerDetected=false;
            else 
                if (Physics2D.BoxCast(transform.position, detectionBox, 0,Vector2.right, detectionOffset,playerMask))
                {
                    _playerDetected=true;
                }
                else _playerDetected=false;

            if (_playerDetected && !_isReloading)
            {
                Fire();
            }
            
        }

        private void Fire()
        {
            if (_leftFaceing)
            {
                GameObject t = Instantiate(cannonBall,new Vector3(transform.position.x-cannonBallOffset,transform.position.y,transform.position.z), Quaternion.identity);
                t.GetComponent<CannonBall>().speed = cannonBallSpeed;
                t.GetComponent<CannonBall>().left = _leftFaceing;
            }
            else
            {
                GameObject t = Instantiate(cannonBall, new Vector3(transform.position.x+cannonBallOffset,transform.position.y,transform.position.z), Quaternion.identity);
                t.GetComponent<CannonBall>().speed = cannonBallSpeed;
                t.GetComponent<CannonBall>().left = _leftFaceing;
            }

            _isReloading=true;
           StartCoroutine(Reload());
        }
        private void OnDrawGizmos()
        {
            if (_leftFaceing)
                Gizmos.DrawWireCube(transform.position-transform.right*detectionOffset,detectionBox);
            else
            {
                Gizmos.DrawWireCube(transform.position+transform.right*detectionOffset,detectionBox);
            }
        }

        IEnumerator Reload()
        {
            yield return new WaitForSeconds(reloadTime);
            _isReloading=false;
        }
    }
}
