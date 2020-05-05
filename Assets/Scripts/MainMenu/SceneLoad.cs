using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Sohyun Yi
public class SceneLoad : MonoBehaviour
{
    private AsyncOperation async;

    public void BtnLoadScene()
    {
        if (async == null)
        {
            Scene currentScene = SceneManager.GetActiveScene(); 
            async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
            async.allowSceneActivation = true;
        }
    }

    public void BtnLoadScene(int i)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(i);
            async.allowSceneActivation = true;
        }
    }

    public void BtnLoadScene(string s)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(s);
            async.allowSceneActivation = true;
        }
    }
}
