using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//by Philipe Gouveia

public class AIUI : MonoBehaviour
{
    [SerializeField] private GameObject aiText;

    private Animation anim;
    [Header("Show")]
    [SerializeField] AnimationClip showAnim;
    [Header("Hide")]
    [SerializeField] AnimationClip hideAnim;

    internal static bool canTalk;
    internal static bool skip;
    string _text;

    #region SFX 
    [Space]
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip textSFX;
    #endregion

    private void Awake()
    {
        sfx = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
        anim.clip = hideAnim;
        anim.Play();
        aiText.GetComponent<Text>().text = "";
        canTalk = true;
        skip = false;
    }

    public void HidePanel()
    {
        sfx.PlayOneShot(textSFX);
        anim.clip = hideAnim;
        anim.Play();
    }

    public void ShowPanel()
    {
        sfx.PlayOneShot(textSFX);
        anim.clip = showAnim;
        anim.Play();
    }

    public void ShowText(string text)
    {
        if (canTalk)
        {
            StartCoroutine(Conversation(text));
        }
    }

    public void ShowText(Queue<string> texts)
    {
        if (canTalk)
        {
            StartCoroutine(Conversation(texts));
        }
    }

    public void Skip()
    {
        skip = true;
    }

    IEnumerator Conversation(string text)
    {
        canTalk = false;
        ShowPanel();
        aiText.GetComponent<Text>().text = "";

        foreach (char item in text)
        {
            if (!skip)
            {
                aiText.GetComponent<Text>().text += item;
                yield return new WaitForSeconds(0.07f);
            }
            else
            {
                aiText.GetComponent<Text>().text = text;
            }
        }
        skip = false;

        yield return new WaitForSeconds(1f);
        HidePanel();
        aiText.GetComponent<Text>().text = "";
        canTalk = true;
        StopCoroutine("Conversation");

    }

    IEnumerator Conversation(Queue<string> texts)
    {
        canTalk = false;
        aiText.GetComponent<Text>().text = "";
        ShowPanel();

        while (texts.Count > 0)
        {

           _text = texts.Dequeue();

            foreach (char item in _text)
            {
                if (!skip)
                {
                    aiText.GetComponent<Text>().text += item;
                    yield return new WaitForSeconds(0.07f);
                }
                else
                {
                    aiText.GetComponent<Text>().text = _text;
                }
            }
            skip = false;

            yield return new WaitForSeconds(1.0f);
            aiText.GetComponent<Text>().text = "";
        }

        HidePanel();
        canTalk = true;
        StopCoroutine("Conversation");
    }
}
