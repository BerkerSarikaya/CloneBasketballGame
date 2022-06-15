using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
     
    void Start()
    {
        
    }

    void PlayerPrefsControl()
    {
        if(PlayerPrefs.HasKey("LEVEL"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("LEVEL"));
        }
        else
        {
            PlayerPrefs.SetInt("LEVEL", 1);
            SceneManager.LoadScene(1);
        }
    }

    
}
