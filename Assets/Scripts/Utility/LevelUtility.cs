using System.Collections;
using UnityEngine;

namespace Utility
{
    public class LevelUtility : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        public void Quit()
        {
            Application.Quit();
        }
    
        private IEnumerator LoadSceneAsync(string sceneName)
        {
            var asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone) yield return null;
        }
    }
}