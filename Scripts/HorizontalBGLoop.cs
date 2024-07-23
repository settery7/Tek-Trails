using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBGLoop : MonoBehaviour
{

    public float moveSpeed = 2f;  // Speed at which the object moves
    public Transform elementTransform;

    void Start()
    {
        if (elementTransform == null)
        {
            elementTransform = GetComponent<Transform>();
        }
    }

    void Update()
    {
        // Move the UI element to the left at a constant speed
        elementTransform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Get the current x position of the UI element
        float currentXPosition = elementTransform.position.x;

        // Check if the UI element's x position is less than -960
        if (currentXPosition < -19.194)
        {
            // Revert the UI element's x position to 960
            elementTransform.position = new Vector3(19.194f, elementTransform.position.y, elementTransform.position.z);
        }
    }
}
