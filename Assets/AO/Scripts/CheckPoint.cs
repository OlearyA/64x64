using UnityEngine;

namespace AO.Scripts
{
    public class CheckPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().CheckPoint(transform.position);
            }
        }
    }
}
