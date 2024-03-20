using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public bool shopOpen = false;
    public GameObject HUD;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject viewControls;
    public GameObject viewStatus;
    public GameObject statusBackButton;

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && optionsMenu.activeSelf && shopOpen == false)
            {
                optionsMenu.SetActive(false);
                pauseMenu.SetActive(true);
            }
            else if (isPaused && viewControls.activeSelf)
            {
                viewControls.SetActive(false);
                pauseMenu.SetActive(true);
            }
            else if (isPaused && viewStatus.activeSelf)
            {
                viewStatus.SetActive(false);
                pauseMenu.SetActive(true);
            }
            else if (isPaused)
            {
                Unpause();
            }
            else if (!isPaused && shopOpen == false)
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (viewStatus.activeSelf)
            {
                StatusUnpause();
            }
            else if (!isPaused && !viewStatus.activeSelf)
            {
                StatusPause();
            }
        }
    }

    public void Pause()
    {
        HUD.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindAnyObjectByType<FirstPersonController>().enabled = false;
        FindAnyObjectByType<GunController>().enabled = false;
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        HUD.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindAnyObjectByType<FirstPersonController>().enabled = true;
        FindAnyObjectByType<GunController>().enabled = true;
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void StatusPause()
    {
        HUD.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindAnyObjectByType<FirstPersonController>().enabled = false;
        FindAnyObjectByType<GunController>().enabled = false;
        isPaused = true;
        viewStatus.SetActive(true);
        statusBackButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void StatusUnpause()
    {
        HUD.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindAnyObjectByType<FirstPersonController>().enabled = true;
        FindAnyObjectByType<GunController>().enabled = true;
        isPaused = false;
        statusBackButton.SetActive(true);
        viewStatus.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
