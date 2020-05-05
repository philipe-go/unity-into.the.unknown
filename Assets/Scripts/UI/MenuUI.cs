using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//by Philipe Gouveia

public class MenuUI : MonoBehaviour
{
    #region UI
    private Animation anim;
    [Header("Show")]
    [SerializeField] AnimationClip showAnim;
    [Header("Hide")]
    [SerializeField] AnimationClip hideAnim;
    public bool isHidden;
    public int counter = 360;
    [Space]
    [Header("Menu Reaction Items")]
    [SerializeField] GameObject paused;
    [SerializeField] GameObject saved;
    [SerializeField] GameObject configPanel;
    [SerializeField] GameObject exitPop;
    [SerializeField] Button firstSelected;
    #endregion

    #region Config Panel
    [Space]
    [Header("Configuration Panel")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider engineerSpeedAnim, musicSlider, sfxSlider;
    [SerializeField] private Text engineerValue, volValue, sfxValue;
    [SerializeField] private string nameParameterMusic, nameParameterSfx;
    #endregion

    private void Awake()
    {
        anim = GetComponent<Animation>();
        isHidden = true;
        anim.clip = hideAnim;
        anim.Play();
        counter = 360;
    }

    private void Start()
    {
        float s = EngineerHandler.speed;
        engineerSpeedAnim.value = s;
        engineerValue.text = ($"{s.ToString()} m/s");

        float v = PlayerPrefs.GetFloat(nameParameterMusic, 0);
        musicSlider.value = v;
        volValue.text = System.Convert.ToInt32(v + 80).ToString();

        float t = PlayerPrefs.GetFloat(nameParameterSfx, 0);
        sfxSlider.value = t;
        sfxValue.text = System.Convert.ToInt32(t + 80).ToString();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            HideBtn();
            configPanel.SetActive(false);
            paused.SetActive(false);
            exitPop.SetActive(false);
            Time.timeScale = 1f;
        }
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }
        if (!isHidden)
        {
            counter--;
            if (counter < 0)
            {
                HideBtn();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Config();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                ExitMenu();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                Restart();
            }
        }
    }

    public void HideBtn()
    {
        if (isHidden) //action to show the UI
        {
            anim.clip = showAnim;
            anim.Play();
            isHidden = false;
        }
        else //action to hide the UI
        {
            if ((!paused.activeSelf) && (!configPanel.activeSelf) && (!exitPop.activeSelf))
            {
                anim.clip = hideAnim;
                anim.Play();
                isHidden = true;
                counter = 360;
                firstSelected.Select();
            }
        }
    }

    public void Pause()
    {
        if ((!configPanel.activeSelf) && (!exitPop.activeSelf))
        {
            paused.SetActive(!paused.activeSelf);
            if (paused.activeSelf) Time.timeScale = 0f;
            else Time.timeScale = 1f;
        }
    }

    public void Config()
    {
        configPanel.SetActive(!configPanel.activeSelf);
        if (configPanel.activeSelf) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }

    public void Save()
    {
        StartCoroutine(Saved());
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void ExitMenu()
    {
        exitPop.SetActive(!exitPop.activeSelf);
        if (exitPop.activeSelf) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        GameManager.SaveGame();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }


    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator Saved()
    {
        saved.SetActive(true);

        GameManager.SaveGame();

        yield return new WaitForSeconds(2.0f);
        saved.SetActive(false);

        yield return null;
        StopCoroutine(Saved());
    }

    public void SetSpeed(float speed)
    {
        Slider slide = engineerSpeedAnim;
        slide.value = speed;
        EngineerHandler.speed = speed;
        FindObjectOfType<EngineerHandler>().anim.SetFloat("WalkSpeed", speed);
        engineerValue.text = ($"{EngineerHandler.speed.ToString("0.#")} m/s");
    }

    public void SetVolumeMusic(float volume)
    {
        Slider slide = musicSlider;
        slide.value = volume;
        volValue.text = System.Convert.ToInt32(volume + 80).ToString();
        audioMixer.SetFloat(nameParameterMusic, volume);
        PlayerPrefs.SetFloat(nameParameterMusic, volume);
        PlayerPrefs.Save();
    }

    public void SetVolumeSFX(float volume)
    {
        Slider slide = sfxSlider;
        slide.value = volume;
        sfxValue.text = System.Convert.ToInt32(volume + 80).ToString();
        audioMixer.SetFloat(nameParameterSfx, volume);
        PlayerPrefs.SetFloat(nameParameterSfx, volume);
        PlayerPrefs.Save();
    }
}
