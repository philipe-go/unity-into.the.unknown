using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//by Philipe Gouveia

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] private Animator zion;
    [SerializeField] private GameObject theEnd;
    [SerializeField] private GameObject[] lights;
    AsyncOperation async;
    private AudioSource audio;
    private AIUI aitalk; 
    public float enterScene;
    public float stop;
    public float talk;
    public float turn;
    public float endscene;

    private AudioSource sfx;
    [SerializeField] private AudioClip footsteps;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
        aitalk = FindObjectOfType<AIUI>();
        Invoke("EnterScene", enterScene);
    }

    private void EnterScene()
    {
        zion.SetBool("enter", true);
        Invoke("StopWalk", stop);
    }

    private void StopWalk()
    {
        zion.SetBool("enter", false);
        Invoke("Talk", talk);
        Invoke("TurnAndWalk", turn);        
    }

    private void Talk()
    {
        aitalk.ShowText($"Hello, {GameManager.engineerName}! \nCongratulations, you have defeated the simulation.\nYou are now free to rest...                                                     ");
    }

    private void TurnAndWalk()
    {
        zion.SetBool("walkAway", true);
        zion.SetBool("endScene", true);
        zion.SetBool("enter", true);
        Invoke("EndScene", endscene);
    }

    private void EndScene()
    {
        theEnd.SetActive(true);
        foreach(GameObject obj in lights)
        {
            obj.SetActive(false);
        }
        async = SceneManager.LoadSceneAsync("MainMenu");
        async.allowSceneActivation = false;
        Invoke("LoadMenu", 10f);
    }

    private void LoadMenu()
    {
        async.allowSceneActivation = true;
    }

    public void Step()
    {
        sfx.PlayOneShot(footsteps);
    }
}
