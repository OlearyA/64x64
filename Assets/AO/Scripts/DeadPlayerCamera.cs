using UnityEngine;

namespace AO.Scripts
{
    public class DeadPlayerCamera : MonoBehaviour
    {
        [SerializeField] private GameObject cinemaCamera;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            CameraManager.Instance.AddCamera(gameObject); 
        }

        public void Destroy()
        {
            Destroy(cinemaCamera);
            Destroy(gameObject);
        }
    }
}
