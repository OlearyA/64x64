using System;
using Unity.VisualScripting;
using UnityEngine;

public class Spike : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         //kill player
         other.gameObject.gameObject.GetComponent<Player>().Alive = false;
      }
   }
}
