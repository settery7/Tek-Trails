using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDropper : MonoBehaviour
{
    private Transform parentTransform;
    [SerializeField] private GameObject rubbish;
    public List<FoodScript> foodscripts = new List<FoodScript>();
    private Vector2 initialPosition = new Vector2(-1.69f, 0.034f);

    // New variables for randomization and tracking
    public float minX = -2f; // Minimum x position
    public float maxX = 2f;  // Maximum x position
    private int foodCount = 0; // Counter for dropped food

    // Timing variables
    public float initialInterval = 4f; // Interval for the first 10 foods
    public float subsequentInterval = 2f; // Interval for the 11th and subsequent foods
    public float minInterval = 0.5f; // Minimum interval for dropping food

    public float initialDropSpeed = 5f; // Initial drop speed
    public float speedIncrement = 3f; // Speed increment

    void Start()
    {
        parentTransform = transform; // Assuming this script is attached to the parent GameObject
        StartCoroutine(DropFoodRoutine());
    }

    IEnumerator DropFoodRoutine()
    {
        float currentInterval = initialInterval;

        while (true)
        {
            generateFood();

            if (foodCount > 5)
            {
                currentInterval = Mathf.Max(currentInterval - 0.5f, minInterval);
            }
            else
            {
                currentInterval = foodCount < 10 ? initialInterval : subsequentInterval;
            }

            yield return new WaitForSeconds(currentInterval);
        }
    }

    void generateFood()
    {
        GameObject waste = Instantiate(rubbish, parentTransform);
        float randomX = Random.Range(minX, maxX);
        waste.transform.localPosition = new Vector2(randomX, initialPosition.y);

        FoodSlot slot = waste.GetComponent<FoodSlot>();

        if (slot != null && foodscripts.Count > 0)
        {
            if (foodCount < 3)
            {
                // Drop edible food for the first three
                slot.AssignFoodScript(foodscripts.Find(food => food.Edible));
            }
            else if (foodCount == 3)
            {
                // Drop inedible food for the fourth
                slot.AssignFoodScript(foodscripts.Find(food => !food.Edible));
            }
            else
            {
                // Drop random food (edible or inedible)
                int randomIndex = Random.Range(0, foodscripts.Count);
                slot.AssignFoodScript(foodscripts[randomIndex]);

                // Increase speed after the 5th run 
                if (foodCount >= 5)
                {
                    slot.dropSpeed += speedIncrement;
                }
            }

            foodCount++;
        }
    }
}
