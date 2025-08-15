using System;
using UnityEngine;

namespace AO.Scripts
{
    public class CannonBall : MonoBehaviour
    {
        private Rigidbody2D rb;
        public bool left;
        public float speed;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * 0.5f, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (left)
            {
                rb.linearVelocityX=-speed;
            }
            else
            {
                rb.linearVelocityX=speed;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.gameObject.GetComponent<Player>().Death();
                //impact sound
                OnDestroy();
            }

            if (other.gameObject.CompareTag("Ground"))
            {
                //impact sound
                OnDestroy();
            }
        }

        private void OnDestroy()
        {
            Destroy(this.gameObject);
        }
    }
}
