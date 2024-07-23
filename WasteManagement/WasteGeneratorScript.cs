using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteGeneratorScript : MonoBehaviour
{
    private Transform parentTransform;
    [SerializeField] private GameObject rubbish;
    public List<TrashScript> trashscripts = new List<TrashScript>();
    private int wasteCount = 0;
    private Vector2 initialPosition = new Vector2(-1.69f, 0.034f);

    void Start()
    {
        parentTransform = transform; // Assuming this script is attached to the parent GameObject
    }

    void Update()
    {
        if (parentTransform.childCount == 0)
        {
            if(parentTransform.childCount < 4)
            {
                generateWaste();
            }   
        }
    }

    void generateWaste()
    {
        for (wasteCount = 0; wasteCount < 4; wasteCount++)
        {
        GameObject waste = Instantiate(rubbish, parentTransform);
        waste.transform.localPosition = new Vector2(initialPosition.x + wasteCount, initialPosition.y);
  
        SlotScript slot = waste.GetComponent<SlotScript>();


            if (slot != null && trashscripts.Count > 0)
            {
                int randomIndex = Random.Range(0, trashscripts.Count);
                slot.AssignTrashScript(trashscripts[randomIndex]);
            }
        }

    }




}
