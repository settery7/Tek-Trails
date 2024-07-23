using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Food")]
[System.Serializable]
public class FoodScript : ScriptableObject
{
    [Header("Food")]
    public Sprite image;
    [TextArea(3, 10)]
    public string ItemDescription;
    public float sate;

    [Header("Food or Not")]
    public bool Edible;
}
