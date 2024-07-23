using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicNavigator : MonoBehaviour
{
    public Image currimage;
    public Sprite Music1;
    public Sprite Music2;
    public Sprite Music3;
    public Sprite Music4;

    public GameObject music1btn, music2btn, music3btn, music4btn;
    public GameObject backButton;
    void Start()
    {
        currimage = GetComponent<Image>();
        currimage.sprite = Music1;
        UpdateButtons();

        /*Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }*/

    }

    public void LeftButton()
    {
        Debug.Log("testleft");
        switch (currimage.sprite.name)
        {
            case "Music1":

                if (AudioManager.instance != null)
                {
                    AudioManager.instance.playMusic("Bgm4");
                }
                currimage.sprite = Music4;
                break;
            case "Music2":
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.playMusic("Bgm");
                }
                currimage.sprite = Music1;
                break;
            case "Music3":
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.playMusic("Bgm2");
                }
                currimage.sprite = Music2;
                break;
            case "Music4":
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.playMusic("Bgm3");
                }
                currimage.sprite = Music3;
                break;
        }


        //StartCoroutine(WaitForSeconds(1f));
        UpdateButtons();
        UpdateBackButton();
        //OnClick();
    }

    public void RightButton()
    {
        Debug.Log("testright");
        switch (currimage.sprite.name)
        {
            case "Music1":

                if(AudioManager.instance != null)
                {
                    AudioManager.instance.playMusic("Bgm4");
                }
                currimage.sprite = Music4;
                break;
            case "Music2":
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.playMusic("Bgm");
                }
                currimage.sprite = Music1;
                break;
            case "Music3":
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.playMusic("Bgm2");
                }
                currimage.sprite = Music2;
                break;
            case "Music4":
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.playMusic("Bgm3");
                }
                currimage.sprite = Music3;
                break;
        }


        //StartCoroutine(WaitForSeconds(2f));
        UpdateButtons();
        UpdateBackButton();
        //OnClick();
    }

  

    private void UpdateButtons()
    {
        music1btn.SetActive(false);
        music2btn.SetActive(false);
        music3btn.SetActive(false);
        music4btn.SetActive(false);


        switch (currimage.sprite.name)
        {
            case "Music1":
                //StartCoroutine(WaitForSeconds(1f));
                music1btn.SetActive(true);
                break;
            case "Music2":
                //StartCoroutine(WaitForSeconds(1f));
                music2btn.SetActive(true);
                break;
            case "Music3":
                //StartCoroutine(WaitForSeconds(1f));
                music3btn.SetActive(true);
                break;
            case "Music4":
                music4btn.SetActive(true);
                break;
        }
    }

    private void UpdateBackButton()
    {
        backButton.SetActive(!backButton.activeSelf);
        Debug.Log("BackButton updated");
    }
}
