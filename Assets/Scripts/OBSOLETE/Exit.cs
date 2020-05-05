using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sohyun Yi
public class Exit : MonoBehaviour
{
    [SerializeField] GameObject quitPanel;

    public void QuitPanel()
    {
        quitPanel.SetActive(true);
    }

    public void ExitQuitPanel()
    {
        quitPanel.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
    }
}
