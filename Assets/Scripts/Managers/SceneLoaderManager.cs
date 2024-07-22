using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager: MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        if (DoesSceneExist(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Сцена с названием " + sceneName + " не существует.");
        }
    }

    public static bool DoesSceneExist(string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            var lastSlash = scenePath.LastIndexOf("/");
            var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);

            if (string.Compare(name, sceneName, true) == 0)
                return true;
        }

        return false;
    }
}
