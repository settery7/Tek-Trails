using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public float moveSpeed = 10f; // Speed at which the camera moves

    public Vector2 minBounds; // Minimum boundaries of the camera
    public Vector2 maxBounds; // Maximum boundaries of the camera

    private Transform cameraTransform;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Fallback to the main camera if none is assigned
        }
        cameraTransform = mainCamera.transform;
    }

    public void MoveCamera(Vector3 direction)
    {
        if (mainCamera != null)
        {
            // Calculate the new camera position
            Vector3 newPosition = cameraTransform.position + direction * moveSpeed * Time.deltaTime;

            // Constrain the new position within the bounds
            newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

            // Apply the constrained position to the camera
            cameraTransform.position = newPosition;
        }
    }
}