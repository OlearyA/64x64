using UnityEngine;
using UnityEngine.SceneManagement;

namespace AO.Scripts
{
    public class SceneManagment : MonoBehaviour
    {
        private static SceneManagment _instance;

        public static SceneManagment Instance { get { return _instance; } }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
        }

        public void MoveToScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
