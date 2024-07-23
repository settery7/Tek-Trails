using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class TrashbinScript : MonoBehaviour
{
    public WasteScoreManager scoreManager;
    public void ReceiveObject(GameObject draggableObject)
    {
        string binname = gameObject.name;
        SlotScript slotscript = draggableObject.GetComponent<SlotScript>();
        TrashScript trashScript = slotscript.script;
        if (trashScript != null)
        {
            // Check the classification of the TrashScript
            if (trashScript.Biodegradable && binname == "Biodegradable")  // Assuming `IsBiodegradable` is the bool property
            {
                scoreManager.addScore();
                Debug.Log("Correctly disposed of biodegradable trash!");
            }
            else if (trashScript.Nonbiodegradable && binname == "NonBiodegradable")
            {
                scoreManager.addScore();
                Debug.Log("Incorrect disposal of non-biodegradable trash!");
            }
            else if (trashScript.Recyclable && binname == "Recyclable")
            {
                scoreManager.addScore();
                Debug.Log("Incorrect disposal of non-biodegradable trash!");
            }
            else
            {
                scoreManager.addmiss();
            }

        }

        Destroy(draggableObject);
    }

}

