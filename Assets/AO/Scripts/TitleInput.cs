using UnityEngine;
using UnityEngine.InputSystem;

namespace AO.Scripts
{
    public class TitleInput : MonoBehaviour
    {
        public string gameplaySceneName;
        public void Title(InputAction.CallbackContext content)
        {
            if (!content.ReadValueAsButton())
            {
                SceneManagment.Instance.MoveToScene(gameplaySceneName);
            }
        }
    }
}
