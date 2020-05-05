using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//by Sidakpreet Singh & Sohyun Yi & Philipe Gouveia

public class MainMenu_Controller : MonoBehaviour
{
    //Sohyun Yi & Sidakpreet & Philipe
    internal static int levelToLoad = 2;
    private string levelselect;
    [SerializeField] private GameObject[] popUpMenus; //phil
    [SerializeField] private Button[] buttons; //phil
    private Color defaultColor;

    private void Start() //phil
    {
        Input.ResetInputAxes();
        defaultColor = buttons[0].colors.normalColor;
    }

    void Update() //Phil
    {
        if (Input.GetButtonDown("Cancel"))
        {
            CloseMenus();
        }
    }


    public void SelectLevelToLoad(string i) //Sidakpreet
    { //phil: changed to string to have it more reliable on the menu
        //levelselect = i;
        SceneManager.LoadScene(i);
    }

    public void CloseMenus() //Phil
    {
        Input.ResetInputAxes();
        foreach (Button btn in buttons)
        {
            btn.enabled = true;
        }

        foreach (GameObject obj in popUpMenus)
        {
            obj.SetActive(false);
        }
        buttons[0].Select();
    }

    #region Main Menu Buttons
    public void Start_Game() //Sidakpreet
    {
        Loadingbar.sceneToLoadName = "FirstStage";
        SceneManager.LoadScene("LoadingScreen");
    }


    public void ResumeGame() //Phil
    {
        FindObjectOfType<GameManager>().LoadGame();
    }

    public void LevelSelectMenu() //Phil
    {
        CloseMenus();
        popUpMenus[1].SetActive(!popUpMenus[1].activeSelf);
    }

    public void OptionMenu() //Sohyun Yi
    {
        //bool state = optionMenu.activeSelf;
        //optionMenu.SetActive(!state);
        CloseMenus();
        popUpMenus[0].SetActive(!popUpMenus[0].activeSelf);
    }

    public void CreditsMenu() //Phil
    {
        CloseMenus();
        popUpMenus[2].SetActive(!popUpMenus[2].activeSelf);
    }

    public void ManualMenu() //Phil
    {
        CloseMenus();
        popUpMenus[3].SetActive(!popUpMenus[3].activeSelf);
    }

    public void QuitMenu() //Sohyun Yi
    {
        CloseMenus();
        popUpMenus[4].SetActive(!popUpMenus[4].activeSelf);
    }

    public void ExitGame() //Sohyun Yi
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
    }
    #endregion
}
