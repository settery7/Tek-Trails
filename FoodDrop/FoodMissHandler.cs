using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodMissHandler : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log("Food detected: " + collision.gameObject.name); // Debug line
            FoodSlot foodSlot = collision.gameObject.GetComponent<FoodSlot>();
            if (foodSlot != null)
            {
                if (foodSlot.food != null && foodSlot.food.Edible)
                {
                    foodSlot.MissFood();
                }
                else
                {
                    foodSlot.JustDestroy();
                }
            }
        }
    }
}
