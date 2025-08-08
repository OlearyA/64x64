using System.Collections.Generic;
using UnityEngine;

namespace AO.Scripts
{
    public class CameraManager : MonoBehaviour
    {
        List<GameObject> cameras = new List<GameObject>();
        private static CameraManager _instance;
        private GameObject _lastAddedCamera;

        public static CameraManager Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }

        public void AddCamera(GameObject camera)
        {
            cameras.Add(camera);
            if (cameras.Count <=1) return;
            DisableMainCamera();
            _lastAddedCamera=camera;
        }

        public void RemoveCamera()
        {
            if (cameras.Count <=1) return;
            EnableMainCamera();
            _lastAddedCamera.GetComponent<DeadPlayerCamera>().Destroy();
            cameras.Remove(_lastAddedCamera);
        }

        private void DisableMainCamera()
        {
            cameras[0].GetComponent<Camera>().enabled = false;
        }
        private void EnableMainCamera()
        {
            cameras[0].GetComponent<Camera>().enabled = true;
        }
    }
}
