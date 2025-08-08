using UnityEngine;

namespace AO.Scripts
{
   public class Spike : MonoBehaviour
   {
      private void OnCollisionEnter2D(Collision2D other)
      {
         if (other.gameObject.CompareTag("Player"))
         {
            //kill player
            other.gameObject.gameObject.GetComponent<Player>().Death();
         }
      }
   }
}
