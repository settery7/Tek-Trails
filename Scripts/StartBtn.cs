using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
    //public string sfxName; // The name of the sound effect to play

    // Start is called before the first frame update
    void Start()
    {
        // Get the Button component and add a listener to the onClick event
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    // This method will be called when the button is clicked
    void OnClick()
    {
        // Play the click sound using the AudioManager
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSfx("Click");

            StartCoroutine(WaitforSeconds(1, 1f));

        }
    }

    private IEnumerator WaitforSeconds(int index, float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(index);
    }
}
