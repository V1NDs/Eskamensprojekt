using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject optionsMenu;

    // Hide options and show main
    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
