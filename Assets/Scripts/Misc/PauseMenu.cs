using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePause = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePause)
            {
                Resume();   
            }

            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePause = false;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePause = true;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        Resume();
        Cursor.visible = true;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Resume();
        Cursor.visible = true;
        Application.Quit();
    }
}
