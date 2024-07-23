using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public CameraMovement cameraMovement; // Reference to the CameraMovement script

    private Image image;
    private bool isHeld = false; // Flag to check if the button is being held
    private Vector3 direction; // Direction to move the camera

    void Start()
    {
        image = GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("Image component not found.");
        }

        // Set the default opacity to 20
        SetOpacity(20);
    }

    void Update()
    {
        if (isHeld)
        {
            cameraMovement.MoveCamera(direction);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set the opacity to 255 on hover
        SetOpacity(255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Set the opacity back to 20 when not hovering
        SetOpacity(20);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Placeholder for single click logic if needed
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Determine direction based on the button name or tag and start moving the camera
        switch (gameObject.name)
        {
            case "UpButton":
                direction = Vector3.up;
                break;
            case "DownButton":
                direction = Vector3.down;
                break;
            case "LeftButton":
                direction = Vector3.left;
                break;
            case "RightButton":
                direction = Vector3.right;
                break;
            default:
                Debug.LogError("Unknown button name.");
                return;
        }
        isHeld = true; // Set the flag to true when button is held
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false; // Reset the flag when button is released
    }

    private void SetOpacity(float alpha)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha / 255f; // Convert to 0-1 range
            image.color = color;
        }
    }
}