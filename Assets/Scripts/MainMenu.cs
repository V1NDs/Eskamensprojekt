using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject optionsMenu;

    // Hide options and show main
    public void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
