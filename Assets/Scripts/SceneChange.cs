using UnityEngine;
using UnityEngine.SceneManagement;
using Unity1Week_20230619;

namespace Common
{
    public class SceneChange : MonoBehaviour
    {

        public void ChangeScene(string input_sceneName)
        {
            SceneManager.LoadScene(input_sceneName);
        }
        public void AddScene(string input_sceneName)
        {
            SceneManager.LoadScene(input_sceneName, LoadSceneMode.Additive);
        }
        public void DeleteScene(string input_sceneName)
        {
            SceneManager.UnloadSceneAsync(input_sceneName);
        }
    }
}