using System;
using UnityEngine;

namespace AO.Scripts
{
    public class CheckPoint : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _audioSource.Play();
                collision.gameObject.GetComponent<Player>().CheckPoint(transform.position);
            }
        }
    }
}
