using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodSlot : MonoBehaviour
{
    public FoodScript food;
    private FoodDropManager scoreManager;
    public float dropSpeed = 5f;

    void Start()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = food.image;
        scoreManager = GameObject.FindGameObjectWithTag("TriggerLogic").GetComponent<FoodDropManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.down * dropSpeed * Time.deltaTime);
    }

    public void AssignFoodScript(FoodScript foodScript)
    {
        food = foodScript;
    }

    public void DestroyFood()
    {
        scoreManager.addScore();
        Destroy(gameObject);
    }

    public void MissFood()
    {
        scoreManager.addmiss();
        Destroy(gameObject);
    }

    public void JustDestroy()
    {
        Destroy(gameObject);
    }
}
