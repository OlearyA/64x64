using UnityEngine;

namespace AO.Scripts
{
    public class DeadPlayerContainer : MonoBehaviour
    {
        [SerializeField]
        GameObject deadPlayer;

        [SerializeField] private Sprite[] deadPlayerSprite;

        public bool flip;
        public int spriteNumber;
        void Start()
        {
            SpriteRenderer spriteRenderer = deadPlayer.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = deadPlayerSprite[spriteNumber];
            spriteRenderer.flipX = flip;
        }
    }
}
