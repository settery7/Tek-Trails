using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyBirbSceneLoader : MonoBehaviour
{

    public GameObject GOpanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if( Input.GetKeyDown(KeyCode.Space) && GOpanel.activeSelf)
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnRoom(string room)
    {
        Debug.Log("triggersad");
        SceneManager.LoadScene(room);
    }
}
