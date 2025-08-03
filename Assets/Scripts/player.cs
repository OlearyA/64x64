using UnityEngine;

namespace Scrips
{
    public class player : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(transform.right * Time.deltaTime);
        }
    }
}
