using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour
{
    WasteScoreManager scoreManager;
    public TrashScript script;
    private bool isDragging = false;
    private Vector2 originalPosition;

    private void Start()
    {
       SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = script.image;
        scoreManager = GameObject.FindGameObjectWithTag("TriggerLogic").GetComponent<WasteScoreManager>();
    }
    void OnMouseDown()
    {
        isDragging = true;
        originalPosition = transform.position;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Check for collision with any receiver
        Collider2D[] hitColliders = Physics2D.OverlapPointAll(transform.position);
        foreach (var hitCollider in hitColliders)
        {
            TrashbinScript receiver = hitCollider.GetComponent<TrashbinScript>();
            if (receiver != null)
            {
                receiver.ReceiveObject(this.gameObject);
                return;
            }
        }

        // Return to original position if not over any receiver
        transform.position = originalPosition;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
    }

    public void AssignTrashScript(TrashScript trashScript)
    {
        script = trashScript;
        // Additional logic to handle the assigned TrashScript
    }
}
