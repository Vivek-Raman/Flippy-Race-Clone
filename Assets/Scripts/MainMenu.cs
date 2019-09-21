using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void UI_Level1()
    {
        SceneManager.LoadScene("Level 1");
    }
    
    public void UI_Level2()
    {
        SceneManager.LoadScene("Level 2");
    }
}
