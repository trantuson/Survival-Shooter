using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneLoader
{
    public static string TargetScene;

    public static void Load(string targetSceneName)
    {
        TargetScene = targetSceneName;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
    }
}
