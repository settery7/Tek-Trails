using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomNavigate : MonoBehaviour
{

    public Image currimage;
    public Sprite Bedroom;
    public Sprite Lounge;
    public Sprite Kitchen;
    public Sprite Outdoor;

    public GameObject Bedroombtns, Loungebtns, Kitchenbtns, Outdoorbtns;
    void Start()
    {
        currimage = GetComponent<Image>();
        currimage.sprite = Bedroom;
        UpdateButtons();

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }

    }

    public void LeftButton()
    {
        Debug.Log("testleft");
        switch (currimage.sprite.name)
        {
            case "BedRoomBG":
                currimage.sprite = Outdoor;
                break;
            case "LoungeBG":
                currimage.sprite = Bedroom;
                break;
            case "KitchenBG":
                currimage.sprite = Lounge;
                break;
            case "OutdoorBG":
                currimage.sprite = Kitchen;
                break;
        }

        
        //StartCoroutine(WaitForSeconds(1f));
        UpdateButtons();
        OnClick();
    }

    public void RightButton()
    {
        Debug.Log("testright");
        switch (currimage.sprite.name)
        {
            case "BedRoomBG":
                currimage.sprite = Lounge;
                break;
            case "LoungeBG":
                currimage.sprite = Kitchen;
                break;
            case "KitchenBG":
                currimage.sprite = Outdoor;
                break;
            case "OutdoorBG":
                currimage.sprite = Bedroom;
                break;
        }

        
        //StartCoroutine(WaitForSeconds(2f));
        UpdateButtons();
        OnClick();
    }

    private void UpdateButtons()
    {
        Bedroombtns.SetActive(false);
        Loungebtns.SetActive(false);
        Kitchenbtns.SetActive(false);
        Outdoorbtns.SetActive(false);

        
        switch (currimage.sprite.name)
        {
            case "BedRoomBG":
                //StartCoroutine(WaitForSeconds(1f));
                Bedroombtns.SetActive(true);
                break;
            case "LoungeBG":
                //StartCoroutine(WaitForSeconds(1f));
                Loungebtns.SetActive(true);
                break;
            case "KitchenBG":
                //StartCoroutine(WaitForSeconds(1f));
                Kitchenbtns.SetActive(true);
                break;
            case "OutdoorBG":
                Outdoorbtns.SetActive(true);
                break;
        }
    }

    public void OnClick()
    {
        // Play the click sound using the AudioManager
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSfx("Click");

        }
    }

    private IEnumerator WaitForSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        //UpdateButtons();
    }

}
