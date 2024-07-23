using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnMute);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMute()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.MuteMusic();

        }
    }
}
