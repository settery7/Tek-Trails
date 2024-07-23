using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingHandler : MonoBehaviour
{
    public Sprite originalSprite;
    public Sprite highlightedSprite;

    private SpriteRenderer spriteRenderer;
    private bool isHovered;
    private bool isPointerOverUI;

    private StatButton JERDANDAGREYT;
    private EnergySystem energySystem;

    private void Awake()
    {
        JERDANDAGREYT = FindAnyObjectByType<StatButton>();

        if (JERDANDAGREYT == null)
        {
            Debug.LogError("StatButton instance not found in the scene.");
        }

        energySystem = FindAnyObjectByType<EnergySystem>();

        if (energySystem == null)
        {
            Debug.LogError("EnergySystem instance not found in the scene.");
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found.");
        }
    }

    void Update()
    {
        isPointerOverUI = IsPointerOverUIElement();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            if (!isHovered && !isPointerOverUI)
            {
                OnPointerEnter();
            }
            isHovered = true;
        }
        else
        {
            if (isHovered || isPointerOverUI)
            {
                OnPointerExit();
            }
            isHovered = false;
        }
    }

    void OnPointerEnter()
    {
        Debug.Log("Pointer Entered: " + gameObject.name);
        spriteRenderer.sprite = highlightedSprite;

        if(energySystem.IsEnergyDepleted())
        {
            spriteRenderer.sprite = originalSprite;
        }
    }

    void OnPointerExit()
    {
        Debug.Log("Pointer Exited: " + gameObject.name);
        spriteRenderer.sprite = originalSprite;
    }

    void OnMouseDown()
    {
        if (energySystem.IsEnergyDepleted())
        {
            Debug.LogWarning("Energy is depleted. Cannot interact with building: " + gameObject.name);
            spriteRenderer.sprite = originalSprite;
            return;
        }

        Debug.Log("Building clicked: " + gameObject.name);
        AddStats();
        // Placeholder for click logic
    }

    private bool IsPointerOverUIElement()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        return results.Count > 0;
    }

    public void AddStats()
    {
        if (JERDANDAGREYT == null)
        {
            Debug.LogError("StatButton is not initialized.");
            return;
        }
        
        switch (gameObject.name)
        {
            case "CIT-NGE":
            case "CIT-RTL":
            case "CIT-Allied":
            case "CIT-SAL":
            case "CIT-Library-Alt":
            case "CIT-GLE":
            case "CIT-Elementary Blg.":
            case "CIT-Elementarym Blg. (1)":
            case "CIT-ACAD":
                JERDANDAGREYT.RaiseIntellect();
                JERDANDAGREYT.RaiseHappiness();
                energySystem.UseEnergy();
                break;
            case "CIT-Canteen":
                JERDANDAGREYT.RaiseHunger();
                JERDANDAGREYT.RaiseHappiness();
                energySystem.UseEnergy();
                break;
            case "CIT-Gym":
                JERDANDAGREYT.RaisePhysical();
                JERDANDAGREYT.RaiseHappiness();
                energySystem.UseEnergy();
                break;
            default:
                Debug.LogWarning("Unknown building name: " + gameObject.name);
                break;
        }
    }
}