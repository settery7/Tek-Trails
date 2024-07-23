using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFood : MonoBehaviour
{
    public float moveSpeed = 10f; // Speed at which the player moves

    private Rigidbody2D rb;
    private Vector2 movement;


    public Animator animator; // Reference to the Animator component
    public string animationBoolName = "Eating"; // The name of the trigger parameter in the Animator
    public bool triggerAnimation = false; // The bool to check for triggering the animation
    private bool IsAlive = true;
    public float delay;
    private FoodDropManager scoreManager;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreManager = GameObject.FindGameObjectWithTag("TriggerLogic").GetComponent<FoodDropManager>();
    }
    void Update()
    {
        // Get input from the player
        float moveX = 0f;

        if (IsAlive)
        {
            if (Input.GetKey(KeyCode.A) && transform.position.x >= -7.829859f)
            {
                moveX = -1f;
            }
            else if (Input.GetKey(KeyCode.D) && transform.position.x <= 7.84471)
            {
                moveX = 1f;
            }

            movement = new Vector2(moveX, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger entered by: " + collision.gameObject.name); // Debug line
        if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log("Food detected: " + collision.gameObject.name); // Debug line
            FoodSlot foodSlot = collision.gameObject.GetComponent<FoodSlot>();
            if (foodSlot != null && IsAlive)
            {
                if (foodSlot.food != null && foodSlot.food.Edible)
                {
                    animator.SetBool(animationBoolName, true); // Set the boolean to true to start the animation
                    foodSlot.DestroyFood(); // Destroy the food object
                    StartCoroutine(ResetAnimationAfterDelay(delay));
                }
                else if (scoreManager.misses >= 8)
                {
                    scoreManager.gameOver();
                    IsAlive = false;
                }
                else
                {
                    scoreManager.gameOver();
                    IsAlive = false;
                }
            }
        }
    }
    private IEnumerator ResetAnimationAfterDelay(float delayMilliseconds)
    {
        float delaySeconds = delayMilliseconds / 1000f; 
        yield return new WaitForSeconds(delaySeconds);
        animator.SetBool(animationBoolName, false); 
    }

    void FixedUpdate()
    {
        // Move the player
        rb.velocity = movement * moveSpeed;
    }
}
