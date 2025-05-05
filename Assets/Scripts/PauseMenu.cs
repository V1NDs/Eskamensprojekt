using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player;
    FPSController characterController;

    public void Quit()
    {
        Application.Quit();
    }

    void Start()
    {
        characterController = player.GetComponent<FPSController>();
    }

    void Update()
    {
        // Toggle on esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }

        // Control player states
        characterController.canMove = !pauseMenu.activeSelf;
        Cursor.visible = pauseMenu.activeSelf;

        if (pauseMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
        }
    }
}
