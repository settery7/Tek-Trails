using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite normalSprite; // The default image
    public Sprite hoverSprite; // The image to show on hover

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("Image component not found.");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image != null)
        {
            image.sprite = hoverSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null)
        {
            image.sprite = normalSprite;
        }
    }
}