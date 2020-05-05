using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

//by Sidakpreet Singh

public class Loadingbar : MonoBehaviour
{
    private AsyncOperation async;

    [SerializeField] private Image loadingbar;
    [SerializeField] private Text txt;
    //internal static int sceneToLoad;
    internal static string sceneToLoadName = null;
    private Scene loadScene;

    void Start()
    {
        loadScene = SceneManager.GetSceneByName(sceneToLoadName);
        
        Time.timeScale = 1.0f;
        Input.ResetInputAxes();

        //Scene currentscene = SceneManager.GetActiveScene();

        System.GC.Collect();

        if (loadScene == null)  
        {
            async = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        }
        else
        {
            async = SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Single);
        }
        async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (loadingbar)  
        {
            loadingbar.fillAmount = async.progress + 0.1f;
        }
        if (txt)  
        {
            txt.text = ((async.progress + 0.1f) * 100).ToString() + "%";
        }
        if (async.progress > 0.89f)  
        {
            async.allowSceneActivation = true;
        }
    }
}
