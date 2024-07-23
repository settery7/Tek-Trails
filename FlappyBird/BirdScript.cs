using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapstr;
    public Changer change;
    public bool birdIsAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        change = GameObject.FindGameObjectWithTag("TriggerLogic").GetComponent<Changer>();

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnSpace);
        }

    }

    void OnSpace()
    {
        // Play the click sound using the AudioManager
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSfx("Jump");
        }
    }

    void OnGameOver()
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.playSfx("GameOver");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(0, 0, 90));

        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive == true)
        {
            myRigidbody.velocity = Vector2.up * flapstr;

            OnSpace();
        }

       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        change.gameOver();
        OnGameOver();
        birdIsAlive = false;
    }
}
