using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Object/Waste")]
[System.Serializable]
public class TrashScript : ScriptableObject
{

    [Header("Waste")]
    public Sprite image;
    [TextArea(3, 10)]
    public string ItemDescription;

    [Header("Waste Type")]
    public bool Biodegradable;
    public bool Nonbiodegradable;
    public bool Recyclable;
    
}
