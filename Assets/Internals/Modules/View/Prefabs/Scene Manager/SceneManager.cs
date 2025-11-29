using UnityEngine;

namespace BasicLegionInfected.View
{
    public class SceneManager : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
		}
    }
}
