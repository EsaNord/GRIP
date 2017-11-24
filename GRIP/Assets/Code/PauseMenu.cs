using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField]
    private GameObject _shade;
    [SerializeField]
    private Button _pauseButton;

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        _shade.SetActive(true);
        _pauseButton.interactable = false;
        Time.timeScale = 0f;        
    }

    public void Continue()
    {
        _pauseMenu.SetActive(false);
        _shade.SetActive(false);
        _pauseButton.interactable = true;
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {        
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
