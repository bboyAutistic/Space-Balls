using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;


	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);

    }


    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (gameIsPaused)
                Resume();

            else
                Pause();
        }
    }


    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        pauseMenuUI.SetActive(true);
		GameObject asdf = GameObject.Find ("NetworkManager");
		if(asdf == null)
			Time.timeScale = 0f;
        gameIsPaused = true;
        
    }


    public void LoadMenu()
    {
        SceneManager.LoadScene("Main_Menu");
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
