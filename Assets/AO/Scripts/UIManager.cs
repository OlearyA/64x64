using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace AO.Scripts
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;

        public static UIManager Instance { get { return _instance; } }
        public int deathCount = 0;
        [FormerlySerializedAs("_deathCountText")] [SerializeField]
        private TextMeshProUGUI deathCountText;
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void AddDeath()
        {
            deathCount++;
            deathCountText.text = "Deaths: " + deathCount.ToString();
        } 

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
