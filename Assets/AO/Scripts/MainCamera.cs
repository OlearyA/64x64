using UnityEngine;

namespace AO.Scripts
{
    public class MainCamera : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            CameraManager.Instance.AddCamera(gameObject); 
        }
    }
}
