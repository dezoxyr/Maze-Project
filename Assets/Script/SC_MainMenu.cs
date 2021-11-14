using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_MainMenu : MonoBehaviour
{
    public GameObject m_MainMenu;
    public GameObject m_CreditsMenu;
    public static int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuPause");
        }
    }

    /// <summary>
    /// Brief called when the user click on a difficulty setting.
    /// 
    /// </summary>
    /// <param name="difficulty">The number of cells in the maze (if 5 then no monster will spawn) -> easy difficulty</param>
    public void PlayNowButton(int difficulty)
    {
        level = difficulty;
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// Brief called when clicking on the credit button
    /// </summary>
    public void CreditsButton()
    {
        // Show Credits Menu
        m_MainMenu.SetActive(false);
        m_CreditsMenu.SetActive(true);
    }

    /// <summary>
    /// Brief called when clicking on the main menu button
    /// </summary>
    public void MainMenuButton()
    {
        // Show Main Menu
        m_MainMenu.SetActive(true);
        m_CreditsMenu.SetActive(false);
    }

    /// <summary>
    /// Brief to load a scene
    /// </summary>
    /// <param name="level">The name of the scene</param>
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    /// <summary>
    /// Brief called when clicking on exit
    /// </summary>
    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}
