using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton Implementation

    public static GameManager Instance
    {
        get
        {
            if (_gm == null)
            {
                _gm = GameObject.FindObjectOfType<GameManager>();
            }
            return _gm;
        }
    }
    private static GameManager _gm;
    
    #endregion
    
    public Text scoreText = null;
    public GameObject retryButton = null;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        retryButton.SetActive(false);
    }

    public int Score { get; private set; } = 0;

    private void Update()
    {
        scoreText.text = Score.ToString();
    }

    public void AddPoint(int pointsToAdd)
    {
        Score += pointsToAdd;
    }

    public void OnPlayerDeath(BoatStateManager player)
    {
        // check and set high score
        if (PlayerPrefs.GetInt("HighScore") < Score)
        {
            PlayerPrefs.SetInt("HighScore", Score);
            PlayerPrefs.Save();
        }
        Destroy(player.gameObject);
        retryButton.SetActive(true);
    }

    public void Retry()
    {
        // reloads scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
    
}