using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void GoRegsiter()
    {
        SceneManager.LoadScene("register");
    }
    public void GoLogin()
    {
        SceneManager.LoadScene("Login");
    }
}
